using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPortal : MonoBehaviour {

    [SerializeField]
    public string sceneToLoad;

    private void Start() {
        if (sceneToLoad.Length == 0) {
            throw new MissingReferenceException("Portal to nowhere! Configure portal in: " + SceneManager.GetActiveScene().name);
        }
    }

    private void Update() {
        transform.Rotate(0f, 0f, 90f * Time.deltaTime);
    }
}
