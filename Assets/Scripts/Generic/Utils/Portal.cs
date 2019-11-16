using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    public Transform dragPosition;
    public Transform spawnPosition;
    [SerializeField] private float dragSpeed = 7f;
    [SerializeField] private string targetScene = "";
    [SerializeField] private string targetElement = "";
    [SerializeField] private string entranceMode = "";
    private GameObject playerTarget;

    void Update() {
        if (playerTarget) {
            playerTarget.transform.position = Vector3.MoveTowards(
                playerTarget.transform.position,
                dragPosition.position,
                Time.deltaTime * dragSpeed
            );
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            if (Director.Instance) {
                playerTarget = collider.gameObject;
                Director.Instance.GoToScene(targetScene, targetElement, entranceMode);
            }
        }
    }
}
