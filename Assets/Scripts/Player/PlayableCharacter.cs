using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackingProperties {
    public float attackCooldown;
    public float specAttackCooldown;
}

public class PlayableCharacter : MonoBehaviour {

    public Animator animator;
    public AttackingProperties attackingProperties;
    public GameObject model;
    public PlayerCollisions playerCollisions;
    public PlayerSFX playerSFX;

    public bool canAttack;
    public bool canSpecAttack;
    protected float attackCountdown;
    protected float specialAttackCountdown;

    void Awake() {
        canAttack = true;
        canSpecAttack = true;
        animator = model.GetComponent<Animator>();
        playerCollisions = transform.parent.GetComponent<PlayerCollisions>();
    }

    protected virtual void _Update() { }

    protected virtual void _Start() { }

    public virtual void Attack() {
        Debug.Log("Unimplemented Attack()");
    }

    public virtual void SpecialAttack() {
        Debug.Log("Unimplemented SpecialAttack()");
    }

    private void Start() {
        _Start();
    }

    private void Update() {
        if (attackCountdown > 0) {
            attackCountdown -= Time.deltaTime;
            if (attackCountdown <= 0) {
                canAttack = true;
            }
        }

        if (specialAttackCountdown > 0) {
            specialAttackCountdown -= Time.deltaTime;
            if (specialAttackCountdown <= 0) {
                canSpecAttack = true;
            }
        }

        _Update();
    }

}
