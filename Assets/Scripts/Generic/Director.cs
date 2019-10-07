using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {
    public static GameObject _Dynamic;
    // Start is called before the first frame update
    void Start() {
        _Dynamic = GameObject.Find("_Dynamic");
    }
}
