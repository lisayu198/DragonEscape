using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 5f;

    public Rigidbody2D rb;  
    public Camera cam;

    [SerializeField] 
    public int maxHealth = 100;
    public int currentHealth;

    Vector2 movement;
    Vector2 mousePos;

    private Vector2 screenBounds;
    private float spriteWidth;
    private float spriteHeight;

    public float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;
    private ScoreManager scoreManager;

    public HealthBar healthBar;

    public Collider2D hole;
    public int dragonDamage = 20;


    private void Start()
    {
        AudioManager.instance.Play("BackgroundMusic");
        rb = GetComponent<Rigidbody2D>();
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene!");
        }
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on the player!");
        }
        currentHealth = maxHealth;
        if (healthBar != null) {
            healthBar.SetMaxHealth(maxHealth);
        } 
        else {
            Debug.LogError("HealthBar is not assigned to the PlayerMovement script!");
        }

        // Calculate screen bounds
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Get sprite dimensions
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x / 2;
        spriteHeight = spriteRenderer.bounds.size.y / 2;


    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        ClampPosition();
    }

    private void ClampPosition()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + spriteWidth, screenBounds.x - spriteWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + spriteHeight, screenBounds.y - spriteHeight);
        transform.position = viewPos;
    }

    public void TakeDamage(int damage)
    {
        AudioManager.instance.Play("DamageSound");
        Debug.Log("TakeDamage called on player. Damage: " + damage + ", Current Health: " + currentHealth);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Debug.Log("Player took " + damage + " damage. Remaining health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void DefeatEnemy(int pointValue)
    {
        AudioManager.instance.Play("DeathSound"); 
        scoreManager.AddPoints(pointValue);
        Debug.Log("Enemy defeated! Points added: " + pointValue);
    }

    void Die()
    {
        AudioManager.instance.Play("DeathSound");
        Debug.Log("Player died!");
        Destroy(gameObject);
        // Update high score before restarting
        if (scoreManager != null)
        {
            scoreManager.UpdateHighScore();
        }
        // Restart the game
        RestartGame();

    }

    void RestartGame()
    {
        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
        {
            return; // Attack is on cooldown
        }

        lastAttackTime = Time.time;

        // Find the nearest dragon within attack range
        Dragon nearestDragon = FindNearestDragon();
        if (nearestDragon != null)
        {
            nearestDragon.TakeDamage(dragonDamage); // Deal 20 damage to the dragon
            Debug.Log("TESTING");
            AudioManager.instance.Play("AttackSound"); // Assuming you have this sound
        }
    }

    private Dragon FindNearestDragon()
    {
        Dragon[] dragons = FindObjectsOfType<Dragon>();
        Dragon nearestDragon = null;
        float nearestDistance = float.MaxValue;

        foreach (Dragon dragon in dragons)
        {
            float distance = Vector2.Distance(transform.position, dragon.transform.position);
            if (distance < nearestDistance)
            {
                nearestDragon = dragon;
                nearestDistance = distance;
            }
        }

        // Check if the nearest dragon is within attack range (e.g., 2 units)
        if (nearestDragon != null && nearestDistance <= 2f)
        {
            return nearestDragon;
        }

        return null;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other == hole) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("next scene");
        }
    }

     public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar.SetHealth(currentHealth);
        Debug.Log($"Player healed. Current health: {currentHealth}");
    }

    // function not needed
    public void powerUp(int damageIncrease)
    {
        dragonDamage = dragonDamage * damageIncrease;
        Debug.Log($"New damage amount to dragon: {dragonDamage}");
    }

}
