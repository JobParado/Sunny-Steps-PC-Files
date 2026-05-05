using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Toggle fpsToggle;

    void Start()
    {
        // Load saved data
        SaveData data = SaveManager.Load();
        fpsToggle.isOn = data.showFPS == 1;

        // Listen for changes
        fpsToggle.onValueChanged.AddListener(OnFPSToggleChanged);
    }

    void OnFPSToggleChanged(bool value)
    {
        SaveData data = SaveManager.Load();
        data.showFPS = value ? 1 : 0;
        SaveManager.Save(data);
    }
}