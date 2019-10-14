using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : Singleton<Director> {
    public static GameObject _Dynamic;
    public static PlayerMovement playerMovement;
    public static GameObject playerObject;

    void Start() {
        _Dynamic = GameObject.Find("_Dynamic");
        playerObject = GameObject.Find("Player");
        if (playerObject) {
            playerMovement = playerObject.GetComponent<PlayerMovement>();
        }
    }
}
