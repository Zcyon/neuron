using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    protected bool isTouchingPlayer;

    [SerializeField] private SpriteRenderer arrowRenderer = null;
    [SerializeField] private SpriteRenderer indicatorRenderer = null;
    private bool interactableBumper;
    private int indicatorFadeFrames = 20;

    protected virtual void _OnTriggerEnter2D(Collider2D collider) { }
    protected virtual void _OnTriggerExit2D(Collider2D collider) { }

    protected virtual void OnInteract() { }

    private void Awake() {
        arrowRenderer.color = new Color(1f, 1f, 1f, 0f);
        indicatorRenderer.color = new Color(1f, 1f, 1f, 0f);
    }

    private void Update() {
        float verticalAxis = Input.GetAxisRaw(GameInput.VERTICAL_AXIS);
        if (!interactableBumper && isTouchingPlayer && verticalAxis < -0.5f) {
            interactableBumper = true;
            OnInteract();
        }
    }

    private IEnumerator IndicatorFadeIn() {
        int frames = 0;
        while (frames < indicatorFadeFrames) {
            ++frames;
            indicatorRenderer.color = new Color(1f, 1f, 1f, (frames * 100f / indicatorFadeFrames) / 100f);
            arrowRenderer.color = new Color(1f, 1f, 1f, (frames * 100f / indicatorFadeFrames) / 100f);
            yield return null;
        }
    }

    private IEnumerator IndicatorFadeOut() {
        int frames = indicatorFadeFrames;
        while (frames > 0) {
            --frames;
            indicatorRenderer.color = new Color(1f, 1f, 1f, (frames * 100f / indicatorFadeFrames) / 100f);
            arrowRenderer.color = new Color(1f, 1f, 1f, (frames * 100f / indicatorFadeFrames) / 100f);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == GameTags.PLAYER_TAG) {
            isTouchingPlayer = true;
            StartCoroutine(IndicatorFadeIn());
            _OnTriggerEnter2D(collider);
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == GameTags.PLAYER_TAG) {
            isTouchingPlayer = false;
            interactableBumper = false;
            StartCoroutine(IndicatorFadeOut());
            _OnTriggerExit2D(collider);
        }
    }
}
