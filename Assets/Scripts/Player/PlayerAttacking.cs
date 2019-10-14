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

        if (Input.GetAxisRaw("SpecialAttackL") != 0 || Input.GetButtonDown("SpecialAttackL")) {
            characterSwitching.ActivateCharacter(CharacterPositions.LEFT);
            characterSwitching.currentPlayableCharacter.SpecialAttack();
        }

        if (Input.GetAxisRaw("SpecialAttackR") != 0 || Input.GetButtonDown("SpecialAttackR")) {
            characterSwitching.ActivateCharacter(CharacterPositions.RIGHT);
            characterSwitching.currentPlayableCharacter.SpecialAttack();
        }
    }
}
