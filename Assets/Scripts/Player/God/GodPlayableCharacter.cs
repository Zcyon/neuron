using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodPlayableCharacter : PlayableCharacter {

    public Animator staffAnimator;
    public GameObject attackProjectile;
    public GameObject cannonL;
    public GameObject cannonR;
    public GameObject specialAttackProjectile;
    public GameObject staffModel;

    private float attackAnimationCooldown = 1f;
    private float attackAnimationCountdown;
    private GameObject boomerangObject;

    protected override void _Update() {
        if (boomerangObject) {
            staffModel.SetActive(false);
        } else {
            staffModel.SetActive(true);
        }

        if (attackAnimationCountdown > 0) {
            attackAnimationCountdown -= Time.deltaTime;
            if (attackAnimationCountdown <= 0) {
                staffAnimator.SetBool("isAttacking", false);
            }
        }
    }

    public override void Attack() {
        if (canAttack) {
            canAttack = false;
            attackCountdown = attackingProperties.attackCooldown;
            attackAnimationCountdown = attackAnimationCooldown;
            Shoot(attackProjectile);
            playerSFX.PlayGodAttackSFX();
            staffAnimator.SetBool("isAttacking", true);
        }
    }

    public override void SpecialAttack() {
        if (canSpecAttack) {
            canSpecAttack = false;
            specialAttackCountdown = attackingProperties.specAttackCooldown;
            GameObject proj = Shoot(specialAttackProjectile);
            Boomerang boomerang = proj.GetComponentInChildren<Boomerang>();
            boomerang.returnPosition = transform;
            boomerangObject = proj;
            playerSFX.PlayGodSpecAttackSFX();
        }
    }

    private GameObject Shoot(GameObject projectile) {
        GameObject cannon = playerCollisions.touchingWallL ? cannonR : cannonL;
        GameObject proj = Instantiate(projectile, cannon.transform.position, cannon.transform.rotation);
        proj.transform.parent = Director.Instance._Dynamic.transform;
        return proj;
    }
}
