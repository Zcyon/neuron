using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseObserver : IObserver<bool> {
    private bool hasConstraints;
    private MonoBehaviour _mono;
    private Rigidbody2D _rigidBody;
    private PauseObservable _subject;
    private RigidbodyConstraints2D constraints2D;
    private Unsubscriber<bool> unsubscriber;
    private Vector2 velocityCache;

    public PauseObserver(PauseObservable subject, Rigidbody2D rigidbody, MonoBehaviour mono) {
        _mono = mono;
        _rigidBody = rigidbody;
        _subject = subject;
        unsubscriber = _subject.Subscribe(this);
    }

    public void OnCompleted() { }
    public void OnError(Exception exception) { }
    public void OnNext(bool value) {
        if (!value) {
            if (hasConstraints) {
                _mono.StartCoroutine(ResumeGameCoroutine());
            }
        } else {
            constraints2D = _rigidBody.constraints;
            hasConstraints = true;
            velocityCache = _rigidBody.velocity;
            _rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void Unsubscribe() {
        unsubscriber.Dispose();
    }

    private IEnumerator ResumeGameCoroutine() {
        int frames = 5;
        while (frames > 0) {
            --frames;
            yield return null;
        }

        _rigidBody.constraints = constraints2D;
        _rigidBody.velocity = velocityCache;
        hasConstraints = false;
    }
}
