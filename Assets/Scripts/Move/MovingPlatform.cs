using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform pointA, pointB;
    public float speed = 2f;

    private Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        // Move between A and B
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }

    // Stick player to platform
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform != null && collision.gameObject.activeInHierarchy)
            {
                collision.transform.SetParent(transform);
            }
        }
    }

    // Unstick when leaving
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Only detach if player still exists and is active
            if (collision.transform != null && collision.gameObject.activeInHierarchy)
            {
                collision.transform.SetParent(null);
            }
        }
    }
}