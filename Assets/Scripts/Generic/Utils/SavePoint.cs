using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : Interactable {
    protected override void OnInteract() {
        Director.Instance.SaveGame();
    }
}
