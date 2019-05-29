using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController : PhysicsObject {

    public float maxSpeed = 10.0f;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private SpriteAnimator spriteAnimator;

    // Use this for initialization
    void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteAnimator = GetComponent<SpriteAnimator>();
    }

    protected override void ComputeVelocity() {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis ("Horizontal");

        if (Input.GetButtonDown ("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonUp ("Jump")) {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        if (velocity.y > jumpTakeOffSpeed) {
            velocity.y = jumpTakeOffSpeed;
        } else if (velocity.y < -jumpTakeOffSpeed) {
            velocity.y = -jumpTakeOffSpeed;
        }
        targetVelocity = move * maxSpeed;
    }

    void LateUpdate () {
        if (targetVelocity.x <= 0.01 && targetVelocity.x >= -0.01) {
            spriteAnimator.changeAnimation("rover_stand");
        } else {
            spriteAnimator.changeAnimation("rover_move");
        }
    }
}
