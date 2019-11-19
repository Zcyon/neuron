using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [HideInInspector] public int health;
    public int maxHealth;

    private PlayerMovement playerMovement;

    void Start() {
        health = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void DamagePlayer(int damage) {
        health -= damage;
    }

    public void KnockoutPlayer() {
        playerMovement.DamageBounce();
    }
}
