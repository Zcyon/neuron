using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    public Transform spawnPosition;
    [SerializeField] private string targetScene = "";
    [SerializeField] private string targetElement = "";
    [SerializeField] private string entranceMode = "";

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            if (Director.Instance) {
                Director.Instance.GoToScene(targetScene, targetElement, entranceMode);
            } else {
                print("Director instance is null!");
            }
        }
    }
}
