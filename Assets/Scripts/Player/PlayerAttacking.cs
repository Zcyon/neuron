using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour {
    private CharacterSwitching characterSwitching;

    void Start() {
        characterSwitching = GetComponent<CharacterSwitching>();
    }

    void Update() {
        if (Input.GetButtonDown("Attack")) {
            characterSwitching.currentPlayableCharacter.Attack();
        }

        print($"SpecialAttackL: {Input.GetAxisRaw("SpecialAttackL")}");
        print($"SpecialAttackR: {Input.GetAxisRaw("SpecialAttackR")}");

        if (Input.GetAxisRaw("SpecialAttackL") != 0) {
            characterSwitching.ActivateCharacter(CharacterPositions.LEFT);
            characterSwitching.currentPlayableCharacter.SpecialAttack();
        }

        if (Input.GetAxisRaw("SpecialAttackR") != 0) {
            characterSwitching.ActivateCharacter(CharacterPositions.RIGHT);
            characterSwitching.currentPlayableCharacter.SpecialAttack();
        }
    }
}
