using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController : PhysicsObject {

    public float maxSpeed = 10.0f;
    public float jumpTakeOffSpeed = 7;

    public int health = 10;
    private int respawn_timer = 0; // should be private
    private bool inactive = false; // should be private
    private float prevGravityModifier = 1.0f;

    private SpriteRenderer spriteRenderer;
    private SpriteAnimator spriteAnimator;

    public GameObject SavePoint;

    // Use this for initialization
    void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteAnimator = GetComponent<SpriteAnimator>();
    }

    protected override void ComputeVelocity() {

        // jumping and movement
        if (inactive == false) {
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

            // save points
            if (grounded && Input.GetKeyDown(KeyCode.DownArrow)) {
                GameObject[] gos;
                gos = GameObject.FindGameObjectsWithTag("SavePoint");
                Collider2D m_Collider = GetComponent<Collider2D>();

                foreach (GameObject go in gos) {
                    Collider2D m_Collider2 = go.GetComponent<Collider2D>();
                    if (m_Collider.bounds.Intersects(m_Collider2.bounds)) {
                        go.GetComponent<SpriteAnimator>().changeAnimation("sp_save");

                        // execute save action here
                        SavePoint = go;
                        break;
                    }
                }
            }

            // test die
            if (Input.GetKeyDown("x")) {
                health = 0;
            }
        }

        // resurrection
        if (health <= 0) {
            respawn_timer += 1;
            if (inactive != true) {
                inactive = true;
                GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                GetComponent<Collider2D>().enabled = false;

                velocity = Vector2.zero;
                prevGravityModifier = gravityModifier;
                gravityModifier = 0.0f;
            }

            if (respawn_timer >= 100) {
                respawn_timer = 0;
                health = 10;
                transform.position = new Vector3(SavePoint.transform.position.x, SavePoint.transform.position.y+1.0f, transform.position.z);
                gravityModifier = prevGravityModifier;
                GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                GetComponent<Collider2D>().enabled = true;
            }
        } else if (inactive != false) {
            inactive = false;
        }
    }

    void LateUpdate () {
        if (targetVelocity.x <= 0.01 && targetVelocity.x >= -0.01) {
            spriteAnimator.changeAnimation("rover_stand");
        } else {
            spriteAnimator.changeAnimation("rover_move");
        }
    }
}
