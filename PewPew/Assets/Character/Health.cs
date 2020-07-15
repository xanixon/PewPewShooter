using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    private float currentHealth;
    private float maxHealth = 10f;
    private bool isDead;

    protected HealthBar healthBar; 

    private void Start() {
        currentHealth = maxHealth;
        healthBar = gameObject.GetComponent<HealthBar>();
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        healthBar.ChangeBar(currentHealth / maxHealth);
        if (currentHealth <= 0) {
            Die();
        }
    }

    public void TakeHeal(float heal) {
        currentHealth += heal;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.ChangeBar(currentHealth / maxHealth);
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }
}
