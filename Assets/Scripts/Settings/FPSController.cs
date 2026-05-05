using UnityEngine;

public class FPSController : MonoBehaviour
{
    public GameObject fpsCounter; // assign your FPS counter UI in Inspector

    void Start()
    {
        SaveData data = SaveManager.Load();
        fpsCounter.SetActive(data.showFPS == 1);
    }
}