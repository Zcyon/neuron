﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerSides {
    public const string LEFT = "left";
    public const string RIGHT = "right";
}

public class PlayerCollisions : MonoBehaviour {

    public bool onGround;
    public bool touchingWallL;
    public bool touchingWallR;
    public Collider2D wallColliderL;
    public Collider2D wallColliderR;
    public GameObject feet;
    public GameObject handL;
    public GameObject handR;
    public float collisionRange;
    public PlayerSFX playerSFX;

    private bool canCollide = true;
    private CharacterSwitching characterSwitching;
    [SerializeField] private Collider2D playerCollider = null;

    void Start() {
        characterSwitching = GetComponent<CharacterSwitching>();
    }

    void Update() {
        CheckGroundCollision();
        CheckWallCollisions(handL, PlayerSides.LEFT);
        CheckWallCollisions(handR, PlayerSides.RIGHT);
        UpdateAnimatorState();
    }

    public void DisableColliders() {
        canCollide = false;
        playerCollider.enabled = false;
    }

    public void EnableColliders() {
        canCollide = true;
        playerCollider.enabled = true;
    }

    private bool CheckGroundCollision() {
        if (!canCollide) {
            return false;
        }

        Vector2 origin = feet.transform.position;
        Vector2 direction = Vector2.down;
        Vector2 size = new Vector2(Math.Abs(transform.localScale.x) / 3f, collisionRange);
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, direction, collisionRange);

        if (hit.collider && (hit.collider.transform.tag == GameTags.FLOOR_TAG || hit.collider.transform.tag == GameTags.WALLS_TAG)) {
            if (onGround == false) {
                playerSFX.PlayLandSFX();
            }
            onGround = true;
            if (characterSwitching.currentPlayableCharacter.animator) {
                characterSwitching.currentPlayableCharacter.animator.SetBool(PlayableCharacterAP.IS_ON_GROUND, onGround);
            }
            return onGround;
        }

        onGround = false;
        if (characterSwitching.currentPlayableCharacter.animator) {
            characterSwitching.currentPlayableCharacter.animator.SetBool(PlayableCharacterAP.IS_ON_GROUND, onGround);
        }
        return onGround;
    }

    private void CheckWallCollisions(GameObject hand, string side = PlayerSides.LEFT) {
        if (!canCollide) {
            return;
        }

        Vector2 origin = hand.transform.position;
        Vector2 direction = hand.transform.right;
        Vector2 size = new Vector2(Math.Abs(transform.localScale.y) / 4f, collisionRange);
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, direction, collisionRange, 1 << LayerMask.NameToLayer(GameLayers.LEVEL_LAYER));

        if (hit.collider) {
            if (side == PlayerSides.LEFT) {
                wallColliderL = hit.collider;
                touchingWallL = true;
            } else if (side == PlayerSides.RIGHT) {
                wallColliderR = hit.collider;
                touchingWallR = true;
            }
        } else {
            if (side == PlayerSides.LEFT) {
                wallColliderL = null;
                touchingWallL = false;
            } else if (side == PlayerSides.RIGHT) {
                wallColliderR = null;
                touchingWallR = false;
            }
        }
    }

    private void UpdateAnimatorState() {
        characterSwitching.currentPlayableCharacter.animator.SetBool(PlayableCharacterAP.IS_TOUCHING_WALL, touchingWallL || touchingWallR);
    }
}
