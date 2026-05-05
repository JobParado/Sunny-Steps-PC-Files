using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 4f;
    public float jumpForce = 10f;
    public float wallSlideSpeed;

    private Animator animator;
    private Rigidbody2D rb;

    private bool isGrounded;
    private bool isTouchingWall;
    private bool facingRight = true;

    private float moveInput;
    private float uiMoveInput;
    private bool jumpQueued;   // queued jump input

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Sets wall slide speed to be half of the jump force for a snappier feel
        wallSlideSpeed = jumpForce * 0.5f;
    }

    void Update()
    {
        // ───── INPUT ─────
        float keyboardInput = Input.GetAxisRaw("Horizontal");
        moveInput = uiMoveInput != 0 ? uiMoveInput : keyboardInput;

        // Queue jump only once per press
        if (Input.GetButtonDown("Jump") && isGrounded)
            jumpQueued = true;

        // ───── MOVE ─────
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // ───── FLIP ─────
        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();

        // ───── GROUND CHECK ─────
        isGrounded = Mathf.Abs(rb.linearVelocity.y) < 0.0000001f;

        // ───── WALL CHECK ─────
        isTouchingWall = Physics2D.Raycast(
            transform.position,
            facingRight ? Vector2.right : Vector2.left,
            0.1f
        );

        // ───── JUMP ─────
        if (jumpQueued && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsFalling", false);
        }
        jumpQueued = false; // consume jump once per frame

        // ───── FALL / SLIDE ─────
        if (!isGrounded && rb.linearVelocity.y < 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsJumping", false);
        }

        if (isTouchingWall && !isGrounded && rb.linearVelocity.y < 0)
        {
            // Clamps the fall speed so it never falls SLOWER than the wallSlideSpeed
            // but also doesn't suddenly "teleport" its speed to a slow value.
            float cappedFallSpeed = Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, cappedFallSpeed);

            animator.SetBool("IsSliding", true);
        }
        else
        {
            animator.SetBool("IsSliding", false);
        }
        if (isTouchingWall && !isGrounded && rb.linearVelocity.y < 0)
{
    // Clamps the fall speed so it never falls SLOWER than the wallSlideSpeed
    // but also doesn't suddenly "teleport" its speed to a slow value.
    float cappedFallSpeed = Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed);
    rb.linearVelocity = new Vector2(rb.linearVelocity.x, cappedFallSpeed);
    
    animator.SetBool("IsSliding", true);
}

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // ───── MOBILE INPUT HOOKS ─────
    public void LeftDown() => uiMoveInput = -1f;
    public void LeftUp() => uiMoveInput = 0f;
    public void RightDown() => uiMoveInput = 1f;
    public void RightUp() => uiMoveInput = 0f;

    public void JumpDown()
    {
        if (isGrounded) jumpQueued = true; // only queue if grounded
    }
    public void JumpUp() { }
}