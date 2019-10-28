using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositioning : MonoBehaviour {

    private int frameAmount = 10;
    void Start() {
        StartCoroutine(PositionPlayerCoroutine());
    }

    private IEnumerator PositionPlayerCoroutine() {
        int frames = 0;
        while (frames < frameAmount) {
            ++frames;
            yield return null;
        }
        PlacePlayer();
    }

    private void PlacePlayer() {
        string sceneTransitionPlayerTarget = Director.Instance.sceneTransitionPlayerTarget;
        string sceneTransitionPlayerEffect = Director.Instance.sceneTransitionPlayerEffect;
        if (sceneTransitionPlayerTarget != null) {
            GameObject portals = GameObject.Find("/Portals");
            Transform target = portals.transform.Find(sceneTransitionPlayerTarget);
            if (target) {
                Portal portal = target.GetComponent<Portal>();
                Director.Instance.playerObject.transform.position = portal.spawnPosition.position;
                if (sceneTransitionPlayerEffect != null) {
                    PlayEffect(sceneTransitionPlayerEffect, portal.spawnPosition);
                }
            }

            Director.Instance.sceneTransitionPlayerTarget = null;
        }
    }

    private void PlayEffect(string effect, Transform target) {
        switch (effect) {
            case "jump":
                Director.Instance.playerMovement.EnterLevelJump(target.right);
                break;
        }
    }
}
