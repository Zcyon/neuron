using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuActions : MonoBehaviour {
    private string titleScreen = "title_screen";
    public void ResumeGame() {
        Director.Instance.PauseGame();
    }

    public void QuitGame() {
        Director.Instance.GoToScene(titleScreen);
    }
}
