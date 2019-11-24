using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence : Singleton<Persistence> {
    private PlayerPersistenceData playerPersistence;

    private void Start() {
        playerPersistence = new PlayerPersistenceData();
        // PersistToDisk();
    }

    public void FetchPersistableData() {
        playerPersistence.scene = Director.Instance.sceneTransitionTarget;
        playerPersistence.health = Director.Instance.playerHealth.health;
        playerPersistence.portal = Director.Instance.sceneTransitionPlayerTarget;
        playerPersistence.entranceMode = Director.Instance.sceneTransitionPlayerEffect;

        // print($"{Director.Instance.playerHealth.health} {Director.Instance.sceneTransitionPlayerTarget} {Director.Instance.sceneTransitionPlayerEffect}");
    }

    public void LoadPersistenceProps() { }

    public void PersistToDisk() {
        print($"{JsonUtility.ToJson(playerPersistence)}");
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
