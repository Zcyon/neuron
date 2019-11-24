using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {
    void Start() {
        Director.Instance.LoadDirectorProps();
        Persistence.Instance.LoadPersistenceProps();
    }
}
