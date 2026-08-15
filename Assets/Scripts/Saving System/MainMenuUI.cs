using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    [Header("Stars")]
    public Image level1Star;
    public Image level2Star;
    public Image level3Star;

    public Sprite filledStar;
    public Sprite emptyStar;
    public Sprite newState;


    void Start()
    {
        SaveData data = SaveManager.Load();

        // Level 1 button: always available
        level1Button.interactable = true;

        // Level 2 button: only if Level 1 beaten
        level2Button.interactable = (data.level1 == 1);

        level3Button.interactable = (data.level2 == 1);
        // Stars
        level1Star.sprite = data.level1 == 1 ? filledStar : emptyStar;
        level2Star.sprite = data.level2 == 1 ? filledStar : emptyStar;
        level3Star.sprite = data.level3 == 1 ? filledStar : emptyStar;

        if (data.level1 == 1)
        {
            level2Button.image.sprite = newState;
        }
        if (data.level2 == 1)
        {
            level3Button.image.sprite = newState;
        }
    }
}