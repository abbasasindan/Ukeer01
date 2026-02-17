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
    public LayerMask groundLayer;   // MUST be set ONLY to the 'Ground' layer in Inspector

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

        // 3. COMBAT LOGIC 
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

    [Header("Combat System")]
public Transform attackPoint; // Reference to a child object at the fist position
public float attackRange = 0.7f;
public LayerMask enemyLayer;

void Attack()
{
    // 1. Sanity check for the pivot
    if (attackPoint == null) return;

    anim.SetTrigger("Attack");
    lastAttackTime = Time.time;

    // 2. Perform a wide spatial query
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

    foreach (Collider2D enemy in hitEnemies)
    {
        // 3. HARDCODED CHECK: Use a Tag or Name filter
        if (enemy.CompareTag("Enemy") || enemy.name.ToLower().Contains("bandit"))
        {
            Debug.Log($"[HARDCODE] Purging {enemy.name} from memory.");
            
            // This is the nuclear option: Full removal from the scene graph
            Destroy(enemy.gameObject); 
        }
    }
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

   void OnDrawGizmosSelected()
{
    if (attackPoint == null) return;
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
}
}