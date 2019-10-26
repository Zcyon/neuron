using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayerOnCollision : MonoBehaviour {
    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == GameTags.PLAYER_TAG) {
            Director.Instance.playerMovement.SetSlowState(false);
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.tag == GameTags.PLAYER_TAG) {
            Director.Instance.playerMovement.SetSlowState(true);
        }
    }
}
