using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackTeleporting : MonoBehaviour {

    public bool isEnabled;
    public GameObject model;
    public GameObject playerTarget;

    private Boomerang boomerang;
    private BoxCollider2D coll;
    private Rigidbody2D rb;

    void Start() {
        boomerang = GetComponent<Boomerang>();
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        isEnabled = true;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (isEnabled && collider.tag == GameTags.FLOOR_TAG) {
            rb.velocity = Vector2.zero;
            coll.enabled = false;
            Director.Instance.playerMovement.Teleport(playerTarget.transform.position + new Vector3(0, playerTarget.transform.localScale.y / 2f, 0));
            model.SetActive(false);
            boomerang.isEnabled = false;
        }
    }
}
