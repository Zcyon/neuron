using System;
using System.Collections.Generic;
using UnityEngine;

public class IObservable<T> {

    private List<IObserver<T>> observers;

    public IObservable() {
        observers = new List<IObserver<T>>();
    }

    public IDisposable Subscribe(IObserver<T> observer) {
        if (!observers.Contains(observer)) {
            observers.Add(observer);
        }
        Debug.Log("subscribed");
        return new Unsubscriber(observers, observer);
    }

    public void Complete() {
        foreach (var observer in observers.ToArray()) {
            if (observers.Contains(observer)) {
                observer.OnCompleted();
            }
        }

        observers.Clear();
    }

    public void Next(T value) {
        foreach (var observer in observers) {
            if (value != null) {
                observer.OnNext(value);
            } else {
                observer.OnError(new Exception());
            }
        }
    }

    private class Unsubscriber : IDisposable {
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
}
