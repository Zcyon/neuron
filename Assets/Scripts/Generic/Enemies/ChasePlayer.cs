using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class ChasePlayer : MonoBehaviour {

    [SerializeField] private BehaviorTree behaviorTree = default;
    [SerializeField] private bool lockXAxis = default;
    [SerializeField] private bool lockYAxis = default;
    [SerializeField] private float chaseTimeout = default;
    [SerializeField] private float footCollisionRange = default;
    [SerializeField] private float handCollisionRange = default;
    [SerializeField] private float speed = default;
    [SerializeField] private Transform foot = default;
    [SerializeField] private Transform hand = default;

    private float chaseCountdown;
    private Rigidbody2D rb;
    private Transform target;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        chaseCountdown = chaseTimeout;
        target = Director.Instance.playerObject.transform;
    }

    void Update() {
        if (chaseCountdown > 0) {
            chaseCountdown -= Time.deltaTime;
            Relocate();
        }

        if (chaseCountdown <= 0) {
            behaviorTree.SetVariableValue("isSeeingPlayer", false);
        }
    }

    void OnEnable() {
        chaseCountdown = chaseTimeout;
        target = Director.Instance.playerObject.transform;
    }

    private void Relocate() {
        Vector2 direction;

        if (lockXAxis) {
            direction = (new Vector2(transform.position.x, target.position.y) - (Vector2) transform.position).normalized;
        } else if (lockYAxis) {
            direction = (new Vector2(target.position.x, transform.position.y) - (Vector2) transform.position).normalized;
        } else {
            direction = (new Vector2(target.position.x, target.position.y) - (Vector2) transform.position).normalized;
        }

        if (CheckCollisionsOnFoot() && !CheckCollisionsOnHand()) {
            rb.velocity = direction * speed;
        } else {
            rb.velocity = Vector2.zero;
        }
    }

    private bool CheckCollisionsOnFoot() {
        Vector2 origin = foot.transform.position;
        Vector2 direction = foot.right;
        Vector2 size = new Vector2(footCollisionRange, Math.Abs(transform.localScale.y) / 2f);
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, direction, footCollisionRange, 1 << LayerMask.NameToLayer(GameLayers.LEVEL_LAYER));
        return hit.collider != null;
    }

    private bool CheckCollisionsOnHand() {
        Vector2 origin = hand.transform.position;
        Vector2 direction = hand.right;
        Vector2 size = new Vector2(handCollisionRange, Math.Abs(transform.localScale.y) / 2f);
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, direction, handCollisionRange, 1 << LayerMask.NameToLayer(GameLayers.LEVEL_LAYER));
        return hit.collider != null;
    }
}
