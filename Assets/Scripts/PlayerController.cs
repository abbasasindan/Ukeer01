using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // === HEADER SETTINGS ===
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Combat Settings")]
    public float attackCooldown = 0.5f; // Time between punches

    [Header("Ground Detection")]
    public Transform groundCheck;   // Drag the 'GroundCheck' object here
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;   // MUST be set ONLY to the 'Ground' layer in Inspector

    // Private variables
    private Rigidbody2D rb;
    private Animator anim;
    private float horizontalInput;
    private bool isGrounded;
    private bool isFacingRight = true;
    private float lastAttackTime;
    private bool isRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. INPUT PROCESSING
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Check if holding Shift to Run
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // 2. JUMP LOGIC (CRITICAL FIX: Bypasses corrupted Input Manager)
        // Uses KeyCode.Space to guarantee one jump per press and fix input chaos.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Apply upward velocity
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            
            // Trigger the jump animation immediately
            anim.SetBool("IsJumping", true); 
        }

        // 3. COMBAT LOGIC 
        // Using explicit key 'J' or 'Fire1' for attack safety
        if (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("Fire1")) 
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
            }
        }

        // 4. VISUALS
        UpdateAnimations();
        FlipSprite();
    }

    void FixedUpdate()
    {
        // PHYSICS CALCULATIONS (Runs 50 times per second)
        
        // Check if feet are touching ground
        // NOTE: LayerMask configuration in Inspector is critical here for the Infinite Jump fix.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Determine current speed (Walk vs Run)
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Apply Movement
        rb.linearVelocity = new Vector2(horizontalInput * currentSpeed, rb.linearVelocity.y);
    }

    void Attack()
    {
        // Optional: Stop moving when attacking
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        // Trigger the animation
        anim.SetTrigger("Attack");
        
        // Reset timer
        lastAttackTime = Time.time;
    }

    void UpdateAnimations()
    {
        // LOGIC FOR ANIMATOR: 0 = Idle, 1 = Walk, 2 = Run
        float animSpeed = 0f;

        if (horizontalInput != 0)
        {
            animSpeed = isRunning ? 2f : 1f;
        }

        anim.SetFloat("Speed", animSpeed);
        
        // === JUMP ANIMATION LOGIC FIX (Solves nonstop animation loop) ===
        // We set IsJumping to FALSE only when the character has landed AND is settled 
        // (vertical velocity is near zero).
        if (isGrounded && rb.linearVelocity.y < 0.1f && rb.linearVelocity.y > -0.1f)
        {
            anim.SetBool("IsJumping", false);
        }
    }

    void FlipSprite()
    {
        // Flips the character to face the direction they are moving
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Draws a Red Circle at the feet in Scene View to help you adjust size
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}