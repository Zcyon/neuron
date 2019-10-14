using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodPlayableCharacter : PlayableCharacter {

    public GameObject attackProjectile;
    public GameObject cannon;
    public GameObject specialAttackProjectile;
    public GameObject staffModel;

    private GameObject boomerangObject;

    protected override void Update() {
        base.Update();
        if (boomerangObject) {
            staffModel.SetActive(false);
        } else {
            staffModel.SetActive(true);
        }
    }

    public override void Attack() {
        if (canAttack) {
            canAttack = false;
            attackCountdown = attackingProperties.attackCooldown;
            Shoot(attackProjectile);
        }
    }

    public override void SpecialAttack() {
        if (canSpecAttack) {
            canSpecAttack = false;
            specialAttackCountdown = attackingProperties.specAttackCooldown;
            GameObject proj = Shoot(specialAttackProjectile);
            Boomerang boomerang = proj.GetComponent<Boomerang>();
            boomerang.returnPosition = transform;
            boomerangObject = proj;
        }
    }

    private GameObject Shoot(GameObject projectile) {
        GameObject proj = Instantiate(projectile, cannon.transform.position, cannon.transform.rotation);
        proj.transform.parent = Director._Dynamic.transform;
        return proj;
    }
}
