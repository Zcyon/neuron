using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackingProperties {
    public float attackCooldown;
}

public class PlayableCharacter : MonoBehaviour {

    public AttackingProperties attackingProperties;

    public virtual void Attack() {
        Debug.Log("Unimplemented Attack()");
    }

    public virtual void SpecialAttack() {
        Debug.Log("Unimplemented SpecialAttack()");
    }
}
