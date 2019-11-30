using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigator : MonoBehaviour {
    [SerializeField] private MenuButton[] menuButtons = null;

    private bool inputBumper = false;
    private int currentButtonIndex;

    protected virtual void _Start() { }

    protected virtual void _Update() { }

    private void OnEnable() {
        HighlightCurrentButton();
    }

    private void Start() {
        _Start();
    }

    private void Update() {
        HandleUserInput();
        _Update();
    }

    private void HandleUserInput() {
        int verticalInput = (int) Input.GetAxisRaw(GameInput.VERTICAL_AXIS);
        int menuVerticalInput = (int) Input.GetAxisRaw(GameInput.MENU_VERTICAL_AXIS);

        verticalInput = verticalInput != 0 ? verticalInput : menuVerticalInput;

        if (inputBumper) {
            if (verticalInput == 0) {
                inputBumper = false;
            }
        } else {
            inputBumper = verticalInput != 0;
            if (verticalInput < 0) {
                if (currentButtonIndex < menuButtons.Length - 1) {
                    ++currentButtonIndex;
                }
            } else if (verticalInput > 0) {
                if (currentButtonIndex > 0) {
                    --currentButtonIndex;
                }
            }

            if (inputBumper) {
                HighlightCurrentButton();
            }
        }
    }

    private void HighlightCurrentButton() {
        menuButtons[currentButtonIndex].Select();
    }
}
