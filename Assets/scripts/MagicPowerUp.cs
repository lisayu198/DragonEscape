using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPowerUp : MonoBehaviour
{
    public int magicPowerUpAmount = 100;
    public GameObject magicBoltPrefab;
    private Bullet magicBolt;
    public float timeLeft = 3.0f;

    void Start() {
        magicBolt = magicBoltPrefab.GetComponent<Bullet>();
        // magicBolt.damage = 20; // makes damage stay at 20
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            // Bullet magic = other.GetComponent<Bullet>();
            if (player != null)
            {
                // Debug.Log("COLLIDED WITH POWERUP");
                //timeLeft = 5.0f;
                Debug.Log("[time test] Time reset to: " + timeLeft);
                magicBolt.powerUp(magicPowerUpAmount);
                
                Destroy(gameObject);
            }
        }
    }


    void FixedUpdate()
    {
        if (timeLeft > 0 && magicBolt.damage >= 100) {
            timeLeft -= Time.fixedDeltaTime;
            // Debug.Log("Time left: " + timeLeft);
        }
        else if (timeLeft <= 0 && magicBolt.damage >= 100) {
            // Debug.Log("Time left: " + timeLeft);
            magicBolt.resetDamage();
            Debug.Log("[time test] Time: " + timeLeft);
            timeLeft = 5.0f;
            Debug.Log("[time test] Time reset: " + timeLeft);
            Debug.Log("[time test] Damage reset to 20");
        }
    
    }
}
