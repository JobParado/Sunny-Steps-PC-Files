// 1/9/2026 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public class FruitCollectible : MonoBehaviour
{
    public AudioClip collectSound; // Public variable for the collect sound
    public Animator animator; // Reference to the Animator component
    public float destroyDelay = 0.5f; // Adjust to match sound length

    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Ensure the AudioSource component exists
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component is missing on " + gameObject.name);
        }

        // Ensure the Animator component exists
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("Animator component is missing on " + gameObject.name);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Play collect sound
            if (audioSource != null && collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
            else
            {
                Debug.LogWarning("AudioSource or collectSound is missing on " + gameObject.name);
            }

            // Play destroy animation
            if (animator != null)
            {
                animator.SetTrigger("Destroy");
            }
            else
            {
                Debug.LogWarning("Animator is missing on " + gameObject.name);
            }

            // Hide visuals immediately (optional)
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
            else
            {
                Debug.LogWarning("SpriteRenderer is missing on " + gameObject.name);
            }

            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            else
            {
                Debug.LogWarning("Collider2D is missing on " + gameObject.name);
            }

            // Destroy the GameObject after the sound finishes playing
            Destroy(gameObject, destroyDelay);
        }
    }
}
