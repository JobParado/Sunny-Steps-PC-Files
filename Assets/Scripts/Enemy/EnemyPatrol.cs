using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform pointA;   // Drag your start point here
    public Transform pointB;   // Drag your end point here

    [Header("Settings")]
    public float speed = 2f;   // Movement speed

    private Transform target;  // Current target point
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Start moving towards point B
        target = pointB;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Move towards the current target
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Flip sprite depending on direction
        if (target.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false; // Facing right
        }
        else if (target.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;  // Facing left
        }

        // Check if bear reached the target
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            // Switch target
            target = (target == pointA) ? pointB : pointA;
        }
    }
}