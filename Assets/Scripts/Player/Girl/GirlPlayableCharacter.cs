using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlPlayableCharacter : PlayableCharacter {

    public GameObject attackSword;
    public PlayerCollisions playerCollisions;
    public PlayerMovement playerMovement;
    public Transform cannonB;
    public Transform cannonL;
    public Transform cannonR;
    public Transform cannonT;

    public override void Attack() {
        if (canAttack) {
            Transform origin = playerMovement.lookingUp
                ? cannonT : playerMovement.lookingDown && !playerCollisions.onGround
                ? cannonB : playerCollisions.touchingWallL && !playerCollisions.onGround ? cannonR : cannonL;
            canAttack = false;
            attackCountdown = attackingProperties.attackCooldown;
            Swipe(attackSword, origin);
        }
    }

    public override void SpecialAttack() {
        Debug.Log("Implemented SpecialAttack() from Girl");
    }

    private void Swipe(GameObject sword, Transform origin) {
        GameObject swordInstance = Instantiate(sword, origin.position, origin.rotation);
        swordInstance.transform.parent = origin.transform;
        playerSFX.PlayGirlAttackSFX();
    }
}
