using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPortal : MonoBehaviour {

    [SerializeField]
    public Object sceneToLoad;

    private void Start() {
        if (!sceneToLoad) {
            throw new MissingReferenceException("Portal to nowhere! Configure portal in: " + SceneManager.GetActiveScene().name);
        }
    }
}
