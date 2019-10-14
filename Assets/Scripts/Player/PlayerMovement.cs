using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float coyoteTime;
    public float damageResponseCooldown;
    public float jumpSpeed;
    public float speed;
    public float teleportSpeed;
    public float wallFrictionSpeed;
    public float wallJumpResponseCooldown;
    public int maxJumps;
    public int mapTileId;

    private bool blocked;
    private CharacterSwitching characterSwitching;
    private float coyoteCountdown;
    private bool forceFall;
    private bool isFalling;
    private bool isTeleporting;
    private bool isWallJumping;
    private bool jump;
    private bool wallJump;
    private int remainingJumps;
    private float damageResponseCountdown;
    private float wallJumpResponseCountdown;
    private PlayerCollisions playerCollisions;
    private Rigidbody2D rb;
    private Vector2 teleportTarget;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerCollisions = GetComponent<PlayerCollisions>();
        characterSwitching = GetComponent<CharacterSwitching>();
        remainingJumps = maxJumps;
    }

    void FixedUpdate() {
        float h = Input.GetAxisRaw(GameInput.HORIZONTAL_AXIS);

        if (!blocked) {
            if (h != 0) {
                RotatePlayer(h);
                isWallJumping = false;
            } else {
                characterSwitching.currentPlayableCharacter.animator.SetBool(PlayableCharacterAP.IS_RUNNING, false);
            }

            if (!isWallJumping) {
                rb.velocity = new Vector2(speed * h, rb.velocity.y);
                characterSwitching.currentPlayableCharacter.animator.SetBool(PlayableCharacterAP.IS_RUNNING, h != 0);
            }
        } else {
            characterSwitching.currentPlayableCharacter.animator.SetBool(PlayableCharacterAP.IS_RUNNING, false);
            if (isWallJumping) {
                RotatePlayer(rb.velocity.x);
            }
        }

        if (jump && !blocked) {
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
            // transform.right = playerCollisions.wallColliderL.transform.position - transform.position;
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
        CheckDamageResponseCooldown();

        if (isTeleporting) {
            transform.position = Vector2.MoveTowards(transform.position, teleportTarget, teleportSpeed * Time.deltaTime);
        }
    }

    public void DamageBounce() {
        if (damageResponseCountdown <= 0) {
            Vector2 direction = transform.right * -1 + transform.up;
            blocked = true;
            damageResponseCountdown = damageResponseCooldown;
            rb.velocity = Vector2.zero;
            rb.velocity = direction * (jumpSpeed / 2f);
        }
    }

    public void Teleport(Vector2 position) {
        blocked = true;
        isTeleporting = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        teleportTarget = position;
    }

    public void StopTeleporting() {
        isTeleporting = false;
        rb.isKinematic = false;
        blocked = false;
        rb.velocity = Vector2.zero;
    }

    private void CheckPlayerInput() {
        if (Input.GetButtonDown(GameInput.JUMP_BUTTON) && remainingJumps > 0) {
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

        if (Input.GetButtonUp(GameInput.JUMP_BUTTON) && !isFalling) {
            forceFall = true;
        }
    }

    private void CheckDamageResponseCooldown() {
        if (damageResponseCountdown > 0) {
            blocked = true;
            damageResponseCountdown -= Time.deltaTime;
            if (damageResponseCountdown <= 0) {
                blocked = false;
            }
        }
    }

    private void CheckWallJumpCooldown() {
        if (wallJumpResponseCountdown > 0) {
            blocked = true;
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
        } else if (!isFalling && rb.velocity.y > 0) {
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
