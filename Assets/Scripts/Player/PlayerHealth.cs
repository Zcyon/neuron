using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [HideInInspector] public int health;
    [HideInInspector] public bool isImmune;
    public int maxHealth;

    private PlayerMovement playerMovement;

    void Start() {
        health = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void DamagePlayer(int damage) {
        if (isImmune) {
            return;
        }
        health -= damage;
    }

    public void KnockoutPlayer() {
        if (isImmune) {
            return;
        }
        playerMovement.DamageBounce();
    }
}
