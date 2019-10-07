using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodPlayableCharacter : PlayableCharacter {

    public GameObject attackProjectile;
    public GameObject cannon;
    public GameObject specialAttackProjectile;

    public override void Attack() {
        GameObject projectile = Instantiate(attackProjectile, cannon.transform.position, cannon.transform.rotation);
        projectile.transform.parent = Director._Dynamic.transform;
        Debug.Log("Implemented Attack() from God");
    }

    public override void SpecialAttack() {
        Debug.Log("Implemented SpecialAttack() from God");
    }
}
