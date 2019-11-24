using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenInit : MonoBehaviour {
    public UIManager ui;

    private void Start() {
        StartCoroutine(TitleScreenIntroCoroutine());
    }

    private IEnumerator TitleScreenIntroCoroutine() {
        int frames = 30;

        ui.FadeIn();

        while (frames > 0) {
            --frames;
            yield return null;
        }

        ui.ShowTitleScreen();
    }
}
