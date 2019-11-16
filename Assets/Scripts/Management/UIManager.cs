using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public Animator transitionFadeAnimator;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void FadeIn() {
        transitionFadeAnimator.SetTrigger("fadeIn");
    }

    public void FadeOut() {
        transitionFadeAnimator.SetTrigger("fadeOut");
    }
}
