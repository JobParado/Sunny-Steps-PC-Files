using UnityEngine;

public class EnemyHeadHitbox : MonoBehaviour
{
    public Animator enemyAnimator;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = enemyAnimator.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int scoreValue = 0;

            if (CompareTag("Dog")) scoreValue += 30;
            else if (CompareTag("Bettle")) scoreValue += 40;
            else if (CompareTag("Slime")) scoreValue += 40;
            else if (CompareTag("Bear")) scoreValue += 50;

                ScoreSystem.instance.AddScore(scoreValue);


            // Play death sound (independent of enemy object)
            if (audioSource != null && audioSource.clip != null)
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

            // Trigger death animation
            enemyAnimator.SetTrigger("Die");

            // Bounce player
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 10f); // use velocity instead of linearVelocity

            // Destroy after animation and sound
            Destroy(enemyAnimator.gameObject, 0.5f); // match with animation/sound length
        }
    }
}