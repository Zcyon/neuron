using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drip : MonoBehaviour {

    public float dropCooldown;
    public GameObject dropPrefab;
    public GameObject spawnPoint;

    private float dropCountdown;

    void Start() {
        dropCountdown = Random.Range(0, dropCooldown * 2);
    }

    void Update() {
        if (dropCountdown > 0) {
            dropCountdown -= Time.deltaTime;
            if (dropCountdown <= 0) {
                Drop();
                dropCountdown = dropCooldown;
            }
        }
    }

    private void Drop() {
        GameObject projectile = Instantiate(dropPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        projectile.transform.parent = Director.Instance._Dynamic.transform;
    }

}
