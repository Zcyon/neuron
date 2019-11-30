using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestString : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SetStringTimeout());
    }

    // Update is called once per frame
    void Update() {

    }

    private IEnumerator SetStringTimeout() {
        float timeout = 1;
        while (timeout > 0) {
            timeout -= Time.deltaTime;
            yield return null;
        }

        Director.Instance.SetString();
    }
}
