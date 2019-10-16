using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag != GameTags.CHECKPOINT_TAG) {
            Destroy(gameObject);
        }
    }
}
