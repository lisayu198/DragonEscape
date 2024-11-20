using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dragon : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float chaseRange = 5f;
    public float fireRate = 1f;
    public GameObject fireballPrefab;
    public Transform firePoint;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private float nextFireTime = 0f;
    public int maxHealth = 100;
    public int pointValue = 500; // Points awarded for defeating the dragon
    private int currentHealth;

    private PlayerMovement player;


    void Start()
    {
        AudioManager.instance.Play("DragonSound");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = playerTransform.GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (playerTransform == null)
        {
            // Player has been destroyed, stop chasing
            rb.velocity = Vector2.zero;
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= chaseRange)
        {
            ChaseAndShoot();
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop moving when player is out of range
        }
    }

    void ChaseAndShoot()
    {
        if (playerTransform == null) return;

        // Chase the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // Rotate to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        // Shoot fireball
        if (Time.time >= nextFireTime)
        {
            ShootFireball();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void ShootFireball()
    {
        AudioManager.instance.Play("Fireball");
        if (playerTransform == null || fireballPrefab == null || firePoint == null) return;

        Vector2 directionToPlayer = (playerTransform.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        GameObject fireballObj = Instantiate(fireballPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
        Fireball fireball = fireballObj.GetComponent<Fireball>();
        if (fireball != null)
        {
            fireball.SetDirection(directionToPlayer);
            Debug.Log("Shot fireball");
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        AudioManager.instance.Play("DragonSound"); 
        Debug.Log("Dragon took damage: " + damage);


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        AudioManager.instance.Play("DeathSound"); 
        if (player != null)
        {
            player.DefeatEnemy(pointValue);
        }
        Debug.Log("Dragon defeated! Points awarded: " + pointValue);
        Destroy(gameObject);
    }


}