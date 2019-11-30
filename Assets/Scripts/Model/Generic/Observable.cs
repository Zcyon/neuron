using System;
using System.Collections.Generic;
using UnityEngine;

public class Observable<T> {

    private List<IObserver<T>> observers;

    public Observable() {
        observers = new List<IObserver<T>>();
    }

    public Unsubscriber<T> Subscribe(IObserver<T> observer) {
        if (!observers.Contains(observer)) {
            observers.Add(observer);
        }
        return new Unsubscriber<T>(observers, observer);
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
}
