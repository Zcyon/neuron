using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenToPause : MonoBehaviour {
    private PauseObserver observer;

    void Start() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb) {
            observer = new PauseObserver(Director.Instance.O_pause, rb, this);
        }
    }

    void OnDestroy() {
        observer.Unsubscribe();
    }
}
