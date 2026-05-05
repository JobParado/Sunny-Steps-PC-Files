using UnityEngine;

public class SpikeHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameManager gameManager = GameManager.Instance;

        if (collision.CompareTag("Player"))
        {
            gameManager.disablePauseButton();

            Animator playerAnimator = collision.GetComponent<Animator>();
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();

            if (playerAnimator != null)
            {
                playerAnimator.SetBool("Die", true);
                if (playerRigidbody != null) playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                if (playerMovement != null) playerMovement.enabled = false;

                float animLength = playerAnimator.GetCurrentAnimatorStateInfo(0).length;

                if (GameManager.Instance != null)
                    GameManager.Instance.TriggerGameOver(animLength);

                Destroy(collision.gameObject, animLength);
            }
        }
    }
}