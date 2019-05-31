using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    private Vector3 initial_offset;
    private float halfHeight;
    private float halfWidth;

    private float prev_frame_x = -1.1f;
    private float prev_frame_y = -1.1f;

    // Start is called before the first frame update
    void Start() {
        initial_offset = new Vector3(0.0f, 4.8f, -10.0f);
        Camera camera = Camera.main;

        // maintains aspect ratio of camera
        float ratio = 0.575f;
        if (Screen.height >= camera.pixelRect.size.x*ratio) {
            float new_width = camera.pixelRect.size.x;
            float new_height = camera.pixelRect.size.x*ratio;
            camera.pixelRect = new Rect((Screen.width-new_width)*0.5f, (Screen.height-new_height)*0.5f, new_width, new_height);
        } else {
            float new_width = camera.pixelRect.size.y/ratio;
            float new_height = camera.pixelRect.size.y;
            camera.pixelRect = new Rect((Screen.width-new_width)*0.5f, (Screen.height-new_height)*0.5f, new_width, new_height);
        }

        // gets variables from camera
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
    }

    // Update is called once per frame
    void LateUpdate() {
        float frame_x = Mathf.Round((player.transform.position.x - initial_offset.x)/(halfWidth*2.0f));
        float frame_y = Mathf.Round((player.transform.position.y - initial_offset.y)/(halfHeight*2.0f));

        if (prev_frame_x != frame_x || prev_frame_y != frame_y) {
            prev_frame_x = frame_x;
            prev_frame_y = frame_y;

            Debug.Log("Frame: "+frame_x.ToString()+", "+frame_y.ToString());
        }

        Vector3 offset = new Vector3(frame_x*halfWidth*2.0f, frame_y*halfHeight*2.0f, -10.0f);
        transform.position = offset + initial_offset;
    }
}
