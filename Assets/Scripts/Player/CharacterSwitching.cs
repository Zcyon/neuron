using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPositions {
    public const string LEFT = "left";
    public const string RIGHT = "right";
}

public class CharacterSwitching : MonoBehaviour {
    public GameObject playableCharacterLObject;
    public GameObject playableCharacterRObject;
    public PlayableCharacter currentPlayableCharacter {
        get {
            return _currentPlayableCharacter;
        }
        set {
            _currentPlayableCharacter = value;
        }
    }
    public string currentPlayableCharacterPos;

    private PlayableCharacter playableCharacterL;
    private PlayableCharacter playableCharacterR;
    private PlayableCharacter _currentPlayableCharacter;

    void Start() {
        playableCharacterR = playableCharacterRObject.GetComponent<PlayableCharacter>();
        playableCharacterL = playableCharacterLObject.GetComponent<PlayableCharacter>();

        ActivateCharacter(CharacterPositions.RIGHT);
    }

    void Update() {
        if (Input.GetButtonDown(GameInput.L_BUMPER_BUTTON)) {
            ActivateCharacter(CharacterPositions.LEFT);
        }

        if (Input.GetButtonDown(GameInput.R_BUMPER_BUTTON)) {
            ActivateCharacter(CharacterPositions.RIGHT);
        }
    }

    public void ActivateCharacter(string position = CharacterPositions.LEFT) {
        if (position == CharacterPositions.LEFT) {
            playableCharacterRObject.SetActive(false);
            playableCharacterLObject.SetActive(true);
            currentPlayableCharacter = playableCharacterL;
            currentPlayableCharacterPos = CharacterPositions.LEFT;
        } else if (position == CharacterPositions.RIGHT) {
            playableCharacterLObject.SetActive(false);
            playableCharacterRObject.SetActive(true);
            currentPlayableCharacter = playableCharacterR;
            currentPlayableCharacterPos = CharacterPositions.RIGHT;
        }
    }
}
