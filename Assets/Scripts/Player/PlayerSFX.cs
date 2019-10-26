using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackSFX {
    public AudioClip girlAttackSFX;
    public AudioClip godAttackSFX;
    public AudioClip godSpecAttackSFX;
}

[System.Serializable]
public class MovementSFX {
    public AudioClip jumpSFX;
    public AudioClip landSFX;
    public AudioClip secondJumpSFX;
}

public class PlayerSFX : MonoBehaviour {
    public AudioSource audioSource;
    public AttackSFX attackSFX;
    public MovementSFX movementSFX;

    void Start() {
        attackSFX.girlAttackSFX.LoadAudioData();
        attackSFX.godAttackSFX.LoadAudioData();
        attackSFX.godSpecAttackSFX.LoadAudioData();
        movementSFX.jumpSFX.LoadAudioData();
        movementSFX.landSFX.LoadAudioData();
        movementSFX.secondJumpSFX.LoadAudioData();
    }

    public void PlayGirlAttackSFX() {
        audioSource.PlayOneShot(attackSFX.girlAttackSFX);
    }

    public void PlayGodAttackSFX() {
        audioSource.PlayOneShot(attackSFX.godAttackSFX);
    }

    public void PlayGodSpecAttackSFX() {
        audioSource.PlayOneShot(attackSFX.godSpecAttackSFX);
    }

    public void PlayJumpSFX() {
        audioSource.PlayOneShot(movementSFX.jumpSFX);
    }

    public void PlayLandSFX() {
        audioSource.PlayOneShot(movementSFX.landSFX);
    }

    public void PlaySecondJumpSFX() {
        audioSource.PlayOneShot(movementSFX.secondJumpSFX);
    }
}
