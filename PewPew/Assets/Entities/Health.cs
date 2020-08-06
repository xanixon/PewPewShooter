using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(HealthBar))]
public class Health : MonoBehaviour {
    private float currentHealth;
    private float maxHealth = 10f;
    private bool isDead;
    public bool tempCruth = false;

    protected HealthBar healthBar; 

    private void Start() {
        currentHealth = maxHealth;
        healthBar = gameObject.GetComponent<HealthBar>();
    }

    public void TakeDamage(float damage) {
        tempCruth = true;
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
        if (!isDead) {
            gameObject.GetComponent<Animator>().SetTrigger("Death");
            isDead = true;
        }
    }
}
