using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public bool isStunned;
    public int health;

    private float stunCountdown;

    void Update() {
        if (stunCountdown > 0) {
            stunCountdown -= Time.deltaTime;
            if (stunCountdown <= 0) {
                isStunned = false;
            }
        }
    }

    public void DamageEnemy(int damage, float stunDuration = 0) {
        stunCountdown = stunDuration;
        isStunned = true;
        health -= damage;

        if (health <= 0) {
            Die();
        }
    }

    public void Knockback(float magnitude) { }

    private void Die() {
        Destroy(gameObject);
    }
}
