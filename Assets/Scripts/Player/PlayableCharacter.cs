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

    protected bool canAttack;
    protected bool canSpecAttack;
    protected float attackCountdown;
    protected float specialAttackCountdown;

    protected virtual void Start() {
        canAttack = true;
        canSpecAttack = true;
        animator = model.GetComponent<Animator>();
    }

    protected virtual void Update() {
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
    }

    public virtual void Attack() {
        Debug.Log("Unimplemented Attack()");
    }

    public virtual void SpecialAttack() {
        Debug.Log("Unimplemented SpecialAttack()");
    }
}
