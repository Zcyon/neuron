using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {

    private PlayerPersistenceData playerPersistenceData;
    void Start() {
        Director.Instance.LoadDirectorProps();
        // Persistence.Instance.FetchPersistableData();
        playerPersistenceData = Persistence.Instance.GetPersistableData();
        SetPlayerProps();
    }

    private void SetPlayerProps() {
        Director.Instance.playerHealth.health = playerPersistenceData.health != 0 ? playerPersistenceData.health : Director.Instance.playerHealth.health;
    }
}
