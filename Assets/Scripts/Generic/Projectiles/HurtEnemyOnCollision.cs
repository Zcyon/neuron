using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemyOnCollision : MonoBehaviour {

    public bool knockback;
    public float hitCooldown;
    public float knockbackMagnitude;
    public int damage;
    public float stunDuration;
    public Transform raycastOrigin;

    private bool canHit = true;
    private float hitCountdown;

    void Update() {
        if (hitCountdown > 0) {
            hitCountdown -= Time.deltaTime;
            canHit = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (canHit && collider.tag == GameTags.ENEMY_TAG) {
            Vector2 origin = raycastOrigin.position;
            Vector2 direction = ((Vector2) collider.transform.position - origin).normalized;
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, 10, 1 << LayerMask.NameToLayer(GameLayers.ENEMIES_LAYER));
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if (hit.collider) {
                print($"{hit.collider.transform.name}");
            }

            if (hit.collider && hit.collider.tag == GameTags.ENEMY_TAG) {
                enemy.DamageEnemy(damage, stunDuration);
                enemy.Knockback(knockbackMagnitude);
                canHit = false;
                hitCountdown = hitCooldown;
            }
        }
    }
}
