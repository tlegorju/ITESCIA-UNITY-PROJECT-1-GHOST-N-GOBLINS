using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;

    public event Action OnDie = delegate { };

    private void Awake()
    {
        health = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ennemi"))
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        OnDie();
        Destroy(gameObject);
    }
}
