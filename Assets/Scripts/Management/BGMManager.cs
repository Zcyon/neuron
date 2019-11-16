using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : Singleton<BGMManager> {
    // Start is called before the first frame update
    void Start() {
        singletonObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }
}
