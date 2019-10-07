using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlPlayableCharacter : PlayableCharacter {
    public override void Attack() {
        Debug.Log("Implemented Attack() from Girl");
    }

    public override void SpecialAttack() {
        Debug.Log("Implemented SpecialAttack() from Girl");
    }
}
