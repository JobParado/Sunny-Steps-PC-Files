using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class FPSSettings : MonoBehaviour  
{
    [Header("Toggle Options")]
    public List<FPSToggleOption> toggleOptions;   // drag toggles here

    void Start()
    {
        int savedFPS = PlayerPrefs.GetInt("FPSChoice", 60); // default 60
        SetDefaultToggle(savedFPS);
        ApplyFPS();

        foreach (var option in toggleOptions)
        {
            option.toggle.onValueChanged.AddListener(delegate { ApplyFPS(); });
        }
    }

    void ApplyFPS()
    {
        var activeOption = toggleOptions.FirstOrDefault(opt => opt.toggle.isOn);
        if (activeOption == null) return;

        Application.targetFrameRate = activeOption.fpsValue;
        PlayerPrefs.SetInt("FPSChoice", activeOption.fpsValue);
        PlayerPrefs.Save();

    }

    void SetDefaultToggle(int fpsValue)
    {
        foreach (var option in toggleOptions)
        {
            option.toggle.isOn = (option.fpsValue == fpsValue);
        }
    }
}

[System.Serializable]
public class FPSToggleOption
{
    public Toggle toggle;   // drag the Toggle GameObject here
    public int fpsValue;    // set FPS value in Inspector (e.g. 30, 60, -1)
}