using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Collectible Settings")]
    public bool playDestroyAnimation = true;
    public AudioClip pickupSound; // assign sound per prefab
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int scoreValue = 0;

            if (CompareTag("Cherry")) scoreValue+= 10;
            else if (CompareTag("Gem")) scoreValue = 20;

            ScoreSystem.instance.AddScore(scoreValue);

            // Play pickup sound
            if (pickupSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            // Handle destroy
            if (playDestroyAnimation && animator != null)
            {
                animator.SetTrigger("Destroy");
                Destroy(gameObject, 0.5f); // delay for animation
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}