using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    private Vector3 initial_offset;
    private float halfHeight;
    private float halfWidth;

    // Start is called before the first frame update
    void Start() {
        initial_offset = transform.position - player.transform.position;

        Camera camera = Camera.main;
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
    }

    // Update is called once per frame
    void LateUpdate() {
        float frame_x = Mathf.Round(player.transform.position.x/(halfWidth*2.0f));
        float frame_y = Mathf.Round(player.transform.position.y/(halfHeight*2.0f));

        Vector3 offset = new Vector3(frame_x*halfWidth*2.0f, frame_y*halfHeight*2.0f, -10.0f);
        transform.position = offset + initial_offset;
    }
}
