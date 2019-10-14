﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int health;
    public int maxHealth;

    private PlayerMovement playerMovement;

    void Start() {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void DamagePlayer(int damage) {
        health -= damage;
        playerMovement.DamageBounce();
    }

    public void KnockoutPlayer() {
        print($"Knocking out player");
    }
}