using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float lifetime;
    public float speed;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        if (speed > 0) {
            rb.velocity = transform.right * speed;
        }
        Destroy(gameObject, lifetime);
    }
}
