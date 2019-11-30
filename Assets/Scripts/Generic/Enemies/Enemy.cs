using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public bool isStunned;
    public GameObject remnantPrefab;
    public int health;

    private float remnantSpeed = 15f;
    private float stunCountdown;
    protected Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

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

    public void Knockback(float magnitude, Vector2 direction) {
        if (rb) {
            rb.velocity = Vector2.zero;
        }
    }

    private void Die() {
        if (remnantPrefab) {
            Vector2 randomDirection = new Vector2(Random.value, Random.value);
            GameObject remnant = Instantiate(remnantPrefab);
            remnant.GetComponent<Rigidbody2D>().AddForce(randomDirection.normalized * remnantSpeed, ForceMode2D.Impulse);
            remnant.transform.position = transform.position;
            remnant.transform.SetParent(Director.Instance._Dynamic.transform);
        }
        Destroy(gameObject);
    }
}
