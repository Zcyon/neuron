using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementSFX {
    public AudioClip jumpSFX;
    public AudioClip landSFX;
    public AudioClip secondJumpSFX;
}

public class PlayerSFX : MonoBehaviour {
    public AudioSource audioSource;
    public MovementSFX movementSFX;

    void Start() {
        movementSFX.jumpSFX.LoadAudioData();
        movementSFX.landSFX.LoadAudioData();
        movementSFX.secondJumpSFX.LoadAudioData();
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
