using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour {

    public bool isEnabled;
    public float flightTime;
    public float speed;
    public Transform returnPosition;

    private bool isReturning;
    private float flightTimeCountdown;
    private Rigidbody2D rb;
    private SpecialAttackTeleporting specialAttackTeleporting;

    void Start() {
        flightTimeCountdown = flightTime;
        rb = GetComponent<Rigidbody2D>();
        specialAttackTeleporting = GetComponent<SpecialAttackTeleporting>();
        rb.velocity = transform.right * speed;
        isEnabled = true;
    }

    void Update() {
        if (isEnabled) {
            if (flightTimeCountdown > 0) {
                flightTimeCountdown -= Time.deltaTime;
                if (flightTimeCountdown <= 0) {
                    StartReturn();
                }
            }

            if (isReturning) {
                transform.right = returnPosition.position - transform.position;
                transform.position = Vector2.MoveTowards(transform.position, returnPosition.position, speed * Time.deltaTime);
            }
        }

    }

    private void StartReturn() {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        isReturning = true;
        specialAttackTeleporting.isEnabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (isEnabled && isReturning && collider.tag == GameTags.PLAYER_TAG) {
            Destroy(gameObject.transform.parent.gameObject);
        } else if (!isReturning && collider.tag != GameTags.PLAYER_TAG && collider.tag != GameTags.CHECKPOINT_TAG) {
            StartReturn();
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (isEnabled && isReturning && collider.tag == GameTags.PLAYER_TAG) {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
