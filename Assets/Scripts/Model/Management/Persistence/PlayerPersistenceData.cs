using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerPersistenceData {
    public int health;
    public string scene;
    public string portal;
    public string entranceMode;

    public PlayerPersistenceData(PlayerPersistenceData data = null) {
        if (data != null) {
            health = data.health;
            scene = data.scene;
            portal = data.portal;
            entranceMode = data.entranceMode;
        }
    }
}
