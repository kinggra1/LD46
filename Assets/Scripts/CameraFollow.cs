using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private PlayerController player;
    private Camera camera;

    private float zOffset = -5f;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindObjectOfType<PlayerController>();
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        camera.orthographicSize = GetZoomDist();
        Vector3 cameraOffset = new Vector3(0f, 0f, zOffset);
        this.transform.position = player.transform.position + cameraOffset;
    }

    private float GetZoomDist() {
        return Mathf.Clamp(player.GetFuel(), 1, 20) / 2;
    }
}
