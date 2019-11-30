using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unsubscriber<T> : IDisposable {
    private List<IObserver<T>> _observers;
    private IObserver<T> _observer;

    public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer) {
        this._observers = observers;
        this._observer = observer;
    }

    public void Dispose() {
        if (_observer != null && _observers.Contains(_observer)) {
            _observers.Remove(_observer);
        }
    }
}
