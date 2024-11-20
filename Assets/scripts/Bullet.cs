using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
   public GameObject hitEffect;
   public int damage = 20;
   public float speed = 20f;
   public float lifetime = 2f; 

    private void Start()
    {
        // damage = 20;
        // Destroy the bullet after its lifetime
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }


   void OnCollisionEnter2D(Collision2D collision)
   {
       // Check if the collision is with an enemy
       Dragon dragon = collision.gameObject.GetComponent<Dragon>();
       if (dragon != null) {
           dragon.TakeDamage(damage);
           Debug.Log("Damage to dragon: " + damage);
       }


       // Instantiate hit effect if specified
       if (hitEffect != null)
       {
           Instantiate(hitEffect, transform.position, Quaternion.identity);
       }


       // Destroy the bullet
       Destroy(gameObject);
   }

   public void powerUp(int damageIncrease)
    {
        damage = damageIncrease;
        Debug.Log($"[time test] New damage amount to dragon: {damage}");
    }

    public void resetDamage() {
        damage = 20;
    }

}


