using UnityEngine;
using System.Collections;

public class BanditPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTime = 1.0f; // Time to pause at each end

    private Vector3 targetPos;
    private bool isWaiting = false;
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        // Start by moving toward Point B
        if (pointB != null)
        {
            targetPos = pointB.position;
        }

        // Professional Check: Ensure Z is zero to avoid depth issues
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void Update()
    {
        if (isWaiting || pointA == null || pointB == null) return;

        // Move the Bandit toward the target
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // FORGIVING CHECK: If within 0.2 units, consider arrived
        if (Vector2.Distance(transform.position, targetPos) < 0.2f)
        {
            StartCoroutine(SwitchDirection());
        }

        // Animation Sync
        if (anim != null) anim.SetFloat("Speed", 1f);
    }

    IEnumerator SwitchDirection()
    {
        isWaiting = true;
        if (anim != null) anim.SetFloat("Speed", 0f); // Stop walking animation

        yield return new WaitForSeconds(waitTime);

        // Swap target position
        targetPos = (targetPos == pointA.position) ? pointB.position : pointA.position;

        Flip();
        isWaiting = false;
    }

    void Flip()
    {
        // Multiply the x scale by -1 to flip the sprite
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}