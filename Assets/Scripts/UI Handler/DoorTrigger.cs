using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public int levelNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Save progress
            SaveData data = SaveManager.Load();

            if (levelNumber == 1) data.level1 = 1;
            if (levelNumber == 2) data.level2 = 1;
            if (levelNumber == 3) data.level3 = 1;

            SaveManager.Save(data);

            // Trigger victory
            if (GameManager.Instance != null)
                GameManager.Instance.TriggerVictory();
        }
    }
}