using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour {
    private CharacterSwitching characterSwitching;

    void Start() {
        characterSwitching = GetComponent<CharacterSwitching>();
    }

    void Update() {
        if (Input.GetButtonDown(GameInput.ATTACK_BUTTON)) {
            characterSwitching.currentPlayableCharacter.Attack();
        }

        if (Input.GetAxisRaw(GameInput.SPECIAL_ATTACK_L_AXIS) != 0 || Input.GetButtonDown(GameInput.SPECIAL_ATTACK_L_BUTTON)) {
            characterSwitching.ActivateCharacter(CharacterPositions.LEFT);
            characterSwitching.currentPlayableCharacter.SpecialAttack();
        }

        if (Input.GetAxisRaw(GameInput.SPECIAL_ATTACK_R_AXIS) != 0 || Input.GetButtonDown(GameInput.SPECIAL_ATTACK_R_BUTTON)) {
            characterSwitching.ActivateCharacter(CharacterPositions.RIGHT);
            characterSwitching.currentPlayableCharacter.SpecialAttack();
        }
    }
}
