using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour {

    public string initialAnimationName = "";
    private string animationName = "";

    private SpriteRenderer spriteRenderer;
    private int frame_buffer_length = 3; // 1 by default
    private int frame_buffer = 0;
    private int frame = 0;
    private int frame_prefix = 25;
    private int animation_length = 10;
    private int frame_suffix = 25;
    private string current_texture = "";
    private string texture_base = "";
    private string next_animation = "";

    void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    void Start() {
        // completes animation parameters for animationName
        changeAnimation(initialAnimationName);
    }

    public void changeAnimation(string newAnimationName) {
        if (newAnimationName != animationName) {
            animationName = newAnimationName;
            frame = 0;
            frame_buffer = 0;

            if (animationName == "rover_stand") {
                texture_base = "char_stand_";
                frame_prefix = 30;
                animation_length = 10;
                frame_suffix = 10;
                frame_buffer_length = 3;
                next_animation = "";
            } else if (animationName == "rover_move") {
                texture_base = "char_walk_";
                frame_prefix = 0;
                animation_length = 4;
                frame_suffix = 0;
                frame_buffer_length = 3;
                next_animation = "";
            } else if (animationName == "star_blink") {
                texture_base = "tile_star_blink_";
                frame_prefix = 15;
                animation_length = 3;
                frame_suffix = 5;
                frame_buffer_length = 4;
                next_animation = "";
            } else if (animationName == "sp_hover") {
                texture_base = "sp_hover_";
                frame_prefix = 0;
                animation_length = 9;
                frame_suffix = 0;
                frame_buffer_length = 3;
                next_animation = "";
            } else if (animationName == "sp_save") {
                texture_base = "sp_save_";
                frame_prefix = 0;
                animation_length = 5;
                frame_suffix = 0;
                frame_buffer_length = 2;
                next_animation = "sp_hover";
            } else if (animationName == "liquid") {
                texture_base = "liquid_000";
                frame_prefix = 0;
                animation_length = 6;
                frame_suffix = 0;
                frame_buffer_length = 4;
                next_animation = "";
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (frame_buffer >= frame_buffer_length) {
            frame_buffer = 0;
            frame += 1;
        } else {
            frame_buffer += 1;
        }
        if (frame >= frame_prefix+frame_suffix+animation_length) {
            frame = 0;

            if (next_animation != "") {
                changeAnimation(next_animation);
            }
        }

        string new_texture = "";
        if (frame >= frame_prefix && frame < frame_prefix+animation_length) {
            int animation_frame = frame - frame_prefix + 1;
            new_texture = texture_base + animation_frame.ToString();
        } else {
            new_texture = texture_base + "1";
        }

        if (current_texture != new_texture) {
            current_texture = new_texture;

            spriteRenderer.sprite = Resources.Load<Sprite>(current_texture);
        }
    }
}
