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

        Reset();
    }

    // Update is called once per frame
    void Update() {
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, TargetZoomDist(), 0.01f);
        Vector3 cameraOffset = new Vector3(0f, 0f, zOffset);
        this.transform.position = player.transform.position + cameraOffset;
    }

    private float TargetZoomDist() {
        return player.GetFuelRatio() * 5f + 5f;
    }

    private void Reset() {
        camera.orthographicSize = TargetZoomDist();
        camera.transform.position = player.transform.position + new Vector3(0f, 0f, zOffset);
    }
}
