using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : Singleton<Director> {
    public static GameObject _Dynamic;
    public static PlayerMovement playerMovement;
    public static PlayerHealth playerHealth;
    public static GameObject playerObject;

    void Start() {
        LoadDirectorProps();
    }

    public static void DamagePlayer(int damage, bool knockout) {
        playerHealth.DamagePlayer(damage);
        if (knockout) {
            playerHealth.KnockoutPlayer();
        }
    }

    private void LoadDirectorProps() {
        _Dynamic = GameObject.Find("_Dynamic");
        playerObject = GameObject.Find("Player");
        if (playerObject) {
            playerHealth = playerObject.GetComponent<PlayerHealth>();
            playerMovement = playerObject.GetComponent<PlayerMovement>();
        }
    }
}
