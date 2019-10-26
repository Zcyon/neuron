using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class Roam : MonoBehaviour {

    [SerializeField] private BehaviorTree behaviorTree = default;
    [SerializeField] private float eyeCollisionRange = 0f;
    [SerializeField] private float footCollisionRange = 0f;
    [SerializeField] private float handCollisionRange = 0f;
    [SerializeField] private float speed = 0f;
    [SerializeField] private Rigidbody2D rb = default;
    [SerializeField] private Transform foot = default;
    [SerializeField] private Transform hand = default;
    [SerializeField] private Transform eye = default;

    void Start() { }

    void Update() {
        rb.velocity = new Vector2((transform.right * speed).x, rb.velocity.y);
        if (CheckCollisionsOnHand() || !CheckCollisionsOnFoot()) {
            transform.right = transform.right * -1;
        }

        if (CheckCollisionsOnEye()) {
            behaviorTree.SetVariableValue("isSeeingPlayer", true);
        }
    }

    void OnDisable() {
        rb.velocity = Vector2.zero;
    }

    private bool CheckCollisionsOnEye() {
        Vector2 origin = eye.transform.position;
        Vector2 direction = eye.right;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, eyeCollisionRange, 1 << LayerMask.NameToLayer(GameLayers.PLAYER_LAYER));
        return hit.collider != null;
    }

    private bool CheckCollisionsOnHand() {
        Vector2 origin = hand.transform.position;
        Vector2 direction = hand.right;
        Vector2 size = new Vector2(handCollisionRange, Math.Abs(transform.localScale.y) / 2f);
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, direction, handCollisionRange, 1 << LayerMask.NameToLayer(GameLayers.LEVEL_LAYER));
        return hit.collider != null;
    }

    private bool CheckCollisionsOnFoot() {
        Vector2 origin = foot.transform.position;
        Vector2 direction = foot.right;
        Vector2 size = new Vector2(footCollisionRange, Math.Abs(transform.localScale.y) / 2f);
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, direction, footCollisionRange, 1 << LayerMask.NameToLayer(GameLayers.LEVEL_LAYER));
        return hit.collider != null;
    }
}
