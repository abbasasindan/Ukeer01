using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Jump Settings")]
    public float coyoteTime = 0.15f;
    private float coyoteTimeCounter;

    [Header("Combat Settings")]
    public float attackCooldown = 0.5f;

    [Header("Ground Detection")]
    public Transform groundCheck;   
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // JUMP LOGIC (Space Bar Only)
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            coyoteTimeCounter = 0f; 
            anim.SetBool("IsJumping", true); 
        }

        // ATTACK LOGIC
        if (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("Fire1")) 
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
            }
        }

        UpdateAnimations();
        FlipSprite();
    }

    void FixedUpdate()
    {
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
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
}