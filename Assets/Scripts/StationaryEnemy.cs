using UnityEngine;

public class StationaryEnemy : MonoBehaviour
{
    [Header("Detection Settings")]
    public float attackRange = 1.5f;
    public LayerMask playerLayer;

    [Header("Combat Settings")]
    public float attackCooldown = 2.0f; 
    private float nextAttackTime;

    private Animator anim;
    private Transform player;

    void Start()
    {
        anim = GetComponent<Animator>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        // 1. Check distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 2. Flip to face player even while patrolling/standing
        FlipTowardsPlayer();

        // 3. Attack if in range and cooldown is over
        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
        }
    }

    void Attack()
    {
        nextAttackTime = Time.time + attackCooldown;
        
        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }
        Debug.Log("Bandit attacks on contact!");
    }

    void FlipTowardsPlayer()
    {
        // If player is to the right, scale X should be negative (based on your sprite)
        // If player is to the left, scale X should be positive
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}