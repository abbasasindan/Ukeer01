using UnityEngine;

public class StationaryEnemy : MonoBehaviour
{
    [Header("Detection Settings")]
    public float attackRange = 2f;
    public LayerMask playerLayer;

    [Header("Combat Settings")]
    public float attackCooldown = 2.0f; // 2 second pause between attacks
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

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Face the player
        FlipTowardsPlayer();

        // Only attack if player is in range AND cooldown has finished
        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Set the timer for the next allowed attack
        nextAttackTime = Time.time + attackCooldown;
        
        anim.SetTrigger("Attack");
        Debug.Log("Enemy swings! Next attack in " + attackCooldown + " seconds.");
    }

    void FlipTowardsPlayer()
    {
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}