using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {
    public GameObject tilePrefab;

    // Start is called before the first frame update
    void Start() {
        var tileSize = tilePrefab.GetComponent<BoxCollider2D>().size.x;

        // for (int i = 0; i < 15; i++) {
        //     var position = new Vector2((i-7)*tileSize, -2*tileSize);
        //     Instantiate(tilePrefab, position, Quaternion.identity);
        // }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
