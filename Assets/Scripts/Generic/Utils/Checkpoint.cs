using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public GameObject respawnPoint;

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.tag == GameTags.PLAYER_TAG) {
            Director.Instance.respawnPosition = respawnPoint.transform.position;
        }
    }
}
