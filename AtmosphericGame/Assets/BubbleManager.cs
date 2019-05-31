using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour {
    public GameObject player;
    public GameObject bubbleTail;
    public GameObject bubbleSideLeft;
    public GameObject bubbleSideRight;
    private Vector3 offset = new Vector3(0.0f, 2.0f, 0.0f);
    private SpriteRenderer spriteRenderer;
    private float bubbleSideSizeX = 0.0f;
    private float bubbleTailSizeY = 0.0f;

    private float targetOpacity = 1.0f;
    private float targetScaleX = 0.01f;
    private float currentOpacity = 0.0f; // 0.0f by default
    private float currentScaleX = 0.01f;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bubbleSideSizeX = bubbleSideLeft.GetComponent<SpriteRenderer>().bounds.size.x;
        bubbleTailSizeY = bubbleTail.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update() {
        targetScaleX = 0.01f;
        targetOpacity = 0.0f;

        // toggling powerups
        if (Input.GetKey("q") && player.GetComponent<RoverController>().health > 0) {
            targetScaleX = 0.8f;
            targetOpacity = 1.0f;
        }
    }

    void LateUpdate() {

        // changes scale
        currentScaleX = currentScaleX + ((targetScaleX - currentScaleX)/4.0f);
        transform.localScale = new Vector3(currentScaleX, 0.45f, 1.0f);

        // changes alpha
        currentOpacity = currentOpacity + ((targetOpacity - currentOpacity)/4.0f);
        var col = new Color(1.0f, 1.0f, 1.0f, currentOpacity);
        spriteRenderer.material.color = col;
        bubbleSideLeft.GetComponent<SpriteRenderer>().material.color = col;
        bubbleSideRight.GetComponent<SpriteRenderer>().material.color = col;
        bubbleTail.GetComponent<SpriteRenderer>().material.color = col;

        // changes positions of elements
        transform.position = offset + player.transform.position;

        Vector3 bubbleTailOffset = new Vector3(0.0f, -(spriteRenderer.bounds.size.y*0.5f) - (bubbleTailSizeY*0.5f), 0.0f);
        bubbleTail.transform.position = transform.position + bubbleTailOffset;

        Vector3 bubbleSideOffset = new Vector3((spriteRenderer.bounds.size.x*0.5f) + (bubbleSideSizeX*0.5f), 0.0f, 0.0f);
        bubbleSideRight.transform.position = transform.position + bubbleSideOffset;
        bubbleSideLeft.transform.position = transform.position - bubbleSideOffset;
    }
}
