using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MovementDirections {
    public const string RIGHT = "RIGHT";
    public const string DOWN = "DOWN";
    public const string UP = "UP";
    public const string LEFT = "LEFT";
}

public class Crawler : MonoBehaviour {

    public bool movingRight;
    public float collisionRange;
    public float rotationCooldown;
    public float speed;
    public Transform bottomFoot;
    public Transform rightFoot;

    private string movementDirection;
    private ConstantForce2D gravityForce;
    private int lastRotationIndex = 0;
    private int rotationIndex = 0;
    private float gravityMagnitude = 9.81f;
    private int[] possibleRotations = {
        0,
        -90,
        -180,
        -270
    };
    private float rotationCountdown;
    private Rigidbody2D rb;

    void Start() {
        if (!movingRight) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -180, transform.eulerAngles.z);
        }
        movementDirection = movingRight ? MovementDirections.RIGHT : MovementDirections.LEFT;
        gravityForce = gameObject.AddComponent<ConstantForce2D>();
        rb = GetComponent<Rigidbody2D>();
        rotationCountdown = rotationCooldown;

    }

    void Update() {
        if (rotationCountdown > 0) {
            rotationCountdown -= Time.deltaTime;
        } else {
            ResolveCollisions();
        }
        rb.velocity = transform.right * speed;
        gravityForce.force = transform.up * -1 * gravityMagnitude;
    }

    void FixedUpdate() {
        if (rotationIndex != lastRotationIndex) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, possibleRotations[rotationIndex]);
            lastRotationIndex = rotationIndex;
        }
    }

    private void ResolveCollisions() {
        bool rightCollision = DetectCollision(rightFoot, false);
        bool bottomCollision = DetectCollision(bottomFoot, true);

        if (bottomCollision) {
            if (rightCollision) {
                print("Minus");
                // if (movingRight) {
                --rotationIndex;
                // } else {
                //     ++rotationIndex;
                // }
                rotationCountdown = rotationCooldown;
            }
        } else {
            // if (movingRight) {
            ++rotationIndex;
            // } else {
            //     --rotationIndex;
            // }
            rotationCountdown = rotationCooldown;
        }

        if (rotationIndex > possibleRotations.Length - 1) {
            rotationIndex = 0;
        } else if (rotationIndex < 0) {
            rotationIndex = possibleRotations.Length - 1;
        }

    }

    private bool DetectCollision(Transform origin, bool isBottom = false) {
        Vector2 direction = origin.right;
        Vector2 size;
        if (isBottom) {
            size = new Vector2(Math.Abs(transform.localScale.x), Math.Abs(transform.localScale.y));
        } else {
            size = new Vector2(collisionRange, collisionRange);
        }
        RaycastHit2D hit = Physics2D.BoxCast(origin.position, size, 0f, direction, collisionRange, 1 << LayerMask.NameToLayer(GameLayers.LEVEL_LAYER));
        return hit.collider != null;
    }
}
