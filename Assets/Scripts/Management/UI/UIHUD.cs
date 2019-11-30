using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHUD : MonoBehaviour {
    [SerializeField] private GameObject emptyHeartPrefab = null;
    [SerializeField] private GameObject fullHeartPrefab = null;
    [SerializeField] private Transform healthPanelTransform = null;

    private int currentHealth;

    private void Start() {
        currentHealth = Director.Instance.playerHealth.health;
        RefreshHealth();
    }

    private void Update() {
        int health = Director.Instance.playerHealth.health;

        if (currentHealth != health) {
            currentHealth = health;
            RefreshHealth();
        }
    }

    private void DestroyHealth() {
        foreach (Transform child in healthPanelTransform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void RefreshHealth() {
        DestroyHealth();
        for (int i = 0; i < currentHealth; ++i) {
            GameObject fullHeart = Instantiate(fullHeartPrefab);
            fullHeart.transform.SetParent(healthPanelTransform);
        }

        for (int i = 0; i < Director.Instance.playerHealth.maxHealth - currentHealth; ++i) {
            GameObject emptyHeart = Instantiate(emptyHeartPrefab);
            emptyHeart.transform.SetParent(healthPanelTransform);
        }
    }
}
