using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Director : Singleton<Director> {
    public GameObject _Dynamic;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public GameObject playerObject;
    public Vector2 respawnPosition;

    private bool isRespawning;
    private string testString;

    void Start() {
        Scene scene = SceneManager.GetActiveScene();
        LoadDirectorProps();
    }

    public void DamagePlayer(int damage, bool knockout) {
        playerHealth.DamagePlayer(damage);
        playerHealth.KnockoutPlayer();
        if (knockout && !isRespawning) {
            StartCoroutine(RespawnPlayer(1));
        }
    }

    private IEnumerator RespawnPlayer(float timeout) {
        isRespawning = true;
        float respawnCountdown = timeout;

        while (respawnCountdown > 0) {
            respawnCountdown -= Time.deltaTime;
            yield return null;
        }

        if (respawnPosition != null) {
            playerMovement.MovePlayer(respawnPosition);
        }
        isRespawning = false;
    }

    private void LoadDirectorProps() {
        _Dynamic = GameObject.Find("_Dynamic");
        playerObject = GameObject.Find("Player");
        if (playerObject) {
            playerHealth = playerObject.GetComponent<PlayerHealth>();
            playerMovement = playerObject.GetComponent<PlayerMovement>();
        }
    }

    public void SetString() {
        testString = "Some test value";
        SceneManager.LoadScene("_test_room");
    }
}
