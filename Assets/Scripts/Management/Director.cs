using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Director : Singleton<Director> {
    public GameObject _Dynamic;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public GameObject playerObject;

    private string testString;

    void Start() {
        Scene scene = SceneManager.GetActiveScene();
        print($"Starting Director. Test value: {testString} {scene.name}");
        LoadDirectorProps();
    }

    public void DamagePlayer(int damage, bool knockout) {
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

    public void SetString() {
        testString = "Some test value";
        SceneManager.LoadScene("_test_room");
    }
}
