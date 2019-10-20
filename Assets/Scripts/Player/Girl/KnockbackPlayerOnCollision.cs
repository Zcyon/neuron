using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackPlayerOnCollision : MonoBehaviour {
    public float knockbackMagnitude;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == GameTags.ENEMY_TAG || collider.tag == GameTags.HITTABLE_HAZARD) {
            Director.Instance.playerMovement.Knockback(knockbackMagnitude, transform.right * -1);
        }
    }
}
