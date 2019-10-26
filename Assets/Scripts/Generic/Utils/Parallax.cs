using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    public Transform mainCamera;
    public float speedCoefficient;
    Vector3 lastpos;

    void Start() {
        lastpos = mainCamera.position;
    }

    void Update() {
        transform.position -= ((lastpos - mainCamera.position) * speedCoefficient);
        lastpos = mainCamera.position;
    }
}
