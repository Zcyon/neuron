using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int health;
    [HideInInspector] public bool isImmune;
    public int maxHealth;

    private PlayerMovement playerMovement;
    private PlayerCollisions playerCollisions;

    void Awake() {
        health = maxHealth;
        playerCollisions = GetComponent<PlayerCollisions>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void DamagePlayer(int damage) {
        if (isImmune) {
            return;
        }
        health -= damage;

        if (health <= 0) {
            KillPlayer();
        }
    }

    public void KillPlayer() {
        playerCollisions.DisableColliders();
        StartCoroutine(LoadGameTimeout());
    }

    public void KnockoutPlayer() {
        if (isImmune) {
            return;
        }
        playerMovement.DamageBounce();
    }

    public void RestoreHealth() {
        health = maxHealth;
    }

    private IEnumerator LoadGameTimeout() {
        yield return new WaitForSeconds(2);
        Director.Instance.LoadGame();
    }
}
