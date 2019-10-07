using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float jumpSpeed;
    public float speed;
    public float wallFrictionSpeed;
    public float wallJumpResponseCooldown;
    public int maxJumps;
    public int mapTileId;

    private bool blocked;
    private bool forceFall;
    private bool isFalling;
    private bool isWallJumping;
    private bool jump;
    private bool wallJump;
    private int remainingJumps;
    private float wallJumpResponseCountdown;
    private PlayerCollisions playerCollisions;
    private Rigidbody2D rb;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerCollisions = GetComponent<PlayerCollisions>();
        remainingJumps = maxJumps;
    }

    void FixedUpdate() {
        float h = Input.GetAxisRaw("Horizontal");

        if (!blocked) {
            if (h != 0) {
                RotatePlayer(h);
                isWallJumping = false;
            }

            if (!isWallJumping) {
                rb.velocity = new Vector2(speed * h, rb.velocity.y);
            }
        } else {
            if (isWallJumping) {
                RotatePlayer(rb.velocity.x);
            }
        }

        if (jump) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        } else if (wallJump) {
            Vector2 direction = transform.right * -1 + (transform.up * 2);
            rb.velocity = Vector2.zero;
            rb.velocity = direction.normalized * jumpSpeed;
            isWallJumping = true;
        }

        if (forceFall) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (!isWallJumping && playerCollisions.touchingWallL) {
            rb.velocity = new Vector2(rb.velocity.x, wallFrictionSpeed);
        }

        forceFall = false;
        jump = false;
        wallJump = false;
    }

    void Update() {
        ResolveFallingStatus();
        ResolveRemainingJumps();
        CheckPlayerInput();
        CheckWallJumpCooldown();
        CheckWallJumpState();
    }

    private void CheckPlayerInput() {
        if (Input.GetButtonDown("Jump") && remainingJumps > 0) {
            --remainingJumps;

            if (playerCollisions.touchingWallL && !playerCollisions.onGround) {
                wallJumpResponseCountdown = wallJumpResponseCooldown;
                blocked = true;
                wallJump = true;
            } else {
                isWallJumping = false;
                jump = true;
            }
        }

        if (Input.GetButtonUp("Jump") && !isFalling) {
            forceFall = true;
        }
    }

    private void CheckWallJumpCooldown() {
        if (wallJumpResponseCountdown > 0) {
            wallJumpResponseCountdown -= Time.deltaTime;
            if (wallJumpResponseCountdown <= 0) {
                blocked = false;
            }
        }
    }

    private void CheckWallJumpState() {
        if (playerCollisions.onGround) {
            isWallJumping = false;
        }
    }

    private void ResolveFallingStatus() {
        if (rb.velocity.y < 0) {
            isFalling = true;
        } else if (rb.velocity.y >= 0) {
            isFalling = false;
        }
    }

    private void ResolveRemainingJumps() {
        if (playerCollisions.onGround || playerCollisions.touchingWallL) {
            remainingJumps = maxJumps;
        } else if (!isFalling) {
            remainingJumps = Mathf.Min(remainingJumps, maxJumps - 1);
        }
    }

    private void RotatePlayer(float x) {
        int roundedRotation = Mathf.RoundToInt(x);
        transform.localRotation = new Quaternion(
            0,
            roundedRotation != 0 ? roundedRotation > 0 ? 0 : -180 : transform.localRotation.y,
            0,
            transform.localRotation.w
        );
    }
}
