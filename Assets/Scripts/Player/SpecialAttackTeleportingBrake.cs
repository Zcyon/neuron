using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackTeleportingBrake : MonoBehaviour {
    public GameObject parentObject;
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == GameTags.PLAYER_TAG) {
            Director.Instance.playerMovement.StopTeleporting();
            Destroy(parentObject);
        }
    }
}
