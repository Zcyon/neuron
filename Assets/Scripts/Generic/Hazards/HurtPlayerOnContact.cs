﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnContact : MonoBehaviour {
    public bool knockout;
    public int damage;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == GameTags.PLAYER_TAG) {
            Director.Instance.DamagePlayer(damage, knockout);
        }
    }
}
