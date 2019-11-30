using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenActions : MonoBehaviour {

    public void ExitGame() {
        print("exit game");
    }

    public void LoadGame() {
        Director.Instance.LoadGame();
    }

    public void StartGame() {
        Director.Instance.StartGame();
    }
}
