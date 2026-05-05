using UnityEngine;
using TMPro;

public class FPSCounterScript : MonoBehaviour
{
    public TMP_Text fpsText; // drag your TextMeshPro text here
    private float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();
    }
}