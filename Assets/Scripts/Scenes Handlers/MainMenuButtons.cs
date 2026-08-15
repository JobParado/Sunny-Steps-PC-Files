using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject mainMenuGroup;
    public GameObject creditsGroup;
    public GameObject selectLevelGroup;
    public GameObject settings;

    public AudioSource currentMusic;
    public AudioClip oldMusic;
    public AudioClip newMusic;

    public void ResetGameData()
    {
        SaveManager.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void showLevels()
    {
        mainMenuGroup.SetActive(false);
        creditsGroup.SetActive(false);
        settings.SetActive(false);
        selectLevelGroup.SetActive(true);
    }

    public void PlayLevel_One()
    {
        SceneManager.LoadScene("SunnySteps-1");
    }
    public void PlayLevel_LevelTwo()
    {
        SceneManager.LoadScene("SunnySteps-2");
    }
    public void PlayLevel_LevelThree()
    {
        SceneManager.LoadScene("SunnySteps-3");
    }


    public void showSettings()
    {
        mainMenuGroup.SetActive(false);
        selectLevelGroup.SetActive(false);
        creditsGroup.SetActive(false);
        settings.SetActive(true);
    }
    public void CreditsBack()
    {
        mainMenuGroup.SetActive(false);
        selectLevelGroup.SetActive(false);
        creditsGroup.SetActive(false);
        settings.SetActive(true);

        currentMusic.Stop();
        currentMusic.clip = oldMusic;
        currentMusic.Play();
        
    }
    public void ShowCredits()
    {
        mainMenuGroup.SetActive(false);
        selectLevelGroup.SetActive(false);
        settings.SetActive(false);

        currentMusic.Stop();
        currentMusic.clip = newMusic;
        currentMusic.Play();
        creditsGroup.SetActive(true);
    }

    public void ShowMainMenu()
    {
        creditsGroup.SetActive(false);
        settings.SetActive(false);
        selectLevelGroup.SetActive(false);
        mainMenuGroup.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}