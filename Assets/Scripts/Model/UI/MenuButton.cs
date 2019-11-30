using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {
    private UnityEngine.UI.Button button;

    void Start() {
        button = GetComponent<UnityEngine.UI.Button>();
    }

    public void Click() {
        button.onClick.Invoke();
    }

    public void Select() {
        if (button) {
            button.Select();
        }
    }
}
