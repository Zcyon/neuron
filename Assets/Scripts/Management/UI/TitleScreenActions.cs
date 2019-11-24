using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenActions : MonoBehaviour {

    private string defaultScene = "meat_10";

    public void ExitGame() {
        print("exit game");
    }

    public void LoadGame() {
        PlayerPersistenceData data = Persistence.Instance.ReadFromDisk<PlayerPersistenceData>(GamePlayerPrefs.PLAYER_DATA);
        if (data.scene == "" || data.scene == null) {
            Director.Instance.GoToScene(defaultScene);
        } else {
            Director.Instance.GoToScene(data.scene, data.portal, data.entranceMode);
        }
    }

    public void StartGame() {
        Director.Instance.GoToScene(defaultScene);
    }
}
