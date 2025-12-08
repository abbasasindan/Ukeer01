using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // === HEADER SETTINGS ===
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Jump Settings")]
    public float coyoteTime = 0.15f; // Time allowed to jump after leaving ground
    private float coyoteTimeCounter;

    [Header("Combat Settings")]
    public float attackCooldown = 0.5f; 

    [Header("Ground Detection")]
    public Transform groundCheck;   
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
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // 2. JUMP LOGIC (Uses Coyote Time for reliable jumps)
        // Jump fires if the Coyote Time hasn't expired (coyoteTimeCounter > 0)
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f)
        {
            // Apply upward velocity
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            
            // Consume the jump time so you can't double jump instantly
            coyoteTimeCounter = 0f; 

            // Force the animation state TRUE immediately upon input
            anim.SetBool("IsJumping", true); 
        }
        
        // 3. TIME SHIFT INPUT (Flashback Gate Fix)
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            // CRITICAL CHECK: Only allow the shift if TimeManager confirms the ability is unlocked
            if (TimeManager.instance != null && TimeManager.instance.IsTimeAbilityUnlocked())
            {
                TimeManager.instance.ToggleRealityShift();
            }
        }

        // 4. COMBAT LOGIC 
        if (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("Fire1")) 
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
            }
        }

        // 5. VISUALS
        UpdateAnimations();
        FlipSprite();
    }

    void FixedUpdate()
    {
        // Check if feet are touching ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- COYOTE TIME TRACKER ---
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime; // Reset counter when grounded
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime; // Count down when airborne
        }
        
        // Determine current speed (Walk vs Run)
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        
        // Apply Movement (Trusting the Static Collider to handle the vertical stop)
        rb.linearVelocity = new Vector2(horizontalInput * currentSpeed, rb.linearVelocity.y);
    }

    void Attack()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        anim.SetTrigger("Attack");
        lastAttackTime = Time.time;
    }

    void UpdateAnimations()
    {
        float animSpeed = 0f;
        if (horizontalInput != 0)
        {
            animSpeed = isRunning ? 2f : 1f;
        }

        anim.SetFloat("Speed", animSpeed);
        
        // LANDING LOGIC: If the player is confirmed grounded AND the Rigidbody is settled
        if (isGrounded && rb.linearVelocity.y <= 0.1f)
        {
            anim.SetBool("IsJumping", false);
        }
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}