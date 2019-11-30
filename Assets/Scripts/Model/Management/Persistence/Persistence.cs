using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence : Singleton<Persistence> {
    private PlayerPersistenceData playerPersistence;

    private void Awake() {
        playerPersistence = new PlayerPersistenceData();
        // PersistToDisk();
    }

    public void FetchPersistableData() {
        if (playerPersistence == null) {
            playerPersistence = new PlayerPersistenceData();
        }

        if (Director.Instance == null) {
            return;
        }

        playerPersistence.scene = Director.Instance.sceneTransitionTarget;
        playerPersistence.health = Director.Instance.playerHealth ? Director.Instance.playerHealth.health : 0;
        playerPersistence.portal = Director.Instance.sceneTransitionPlayerTarget;
        playerPersistence.entranceMode = Director.Instance.sceneTransitionPlayerEffect;
    }

    public PlayerPersistenceData GetPersistableData() {
        return playerPersistence;
    }

    public void PersistToDisk() {
        PlayerPrefs.SetString(GamePlayerPrefs.PLAYER_DATA, JsonUtility.ToJson(playerPersistence));
    }

    public T ReadFromDisk<T>(string key) {
        string serialized = PlayerPrefs.GetString(key);
        if (serialized != "") {
            return JsonUtility.FromJson<T>(serialized);
        } else {
            return default(T);
        }
    }
}
