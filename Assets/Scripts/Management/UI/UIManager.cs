using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public Animator transitionFadeAnimator;
    public GameObject pauseMenu;
    public GameObject titleScreen;

    void Start() {
        HidePauseMenu();
        HideTitleScreen();
    }

    public void FadeIn() {
        transitionFadeAnimator.SetTrigger("fadeIn");
    }

    public void FadeOut() {
        transitionFadeAnimator.SetTrigger("fadeOut");
    }

    public void HidePauseMenu() {
        pauseMenu.SetActive(false);
    }

    public void HideTitleScreen() {
        titleScreen.SetActive(false);
    }

    public void ShowPauseMenu() {
        pauseMenu.SetActive(true);
    }

    public void ShowTitleScreen() {
        titleScreen.SetActive(true);
    }
}
