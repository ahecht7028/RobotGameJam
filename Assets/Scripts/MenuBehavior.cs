using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    GameObject mainMenuButton;
    GameObject backButton;
    GameObject title;

    void Start()
    {
        mainMenuButton = transform.Find("MainMenuButtons").gameObject;
        backButton = transform.Find("BackButton").gameObject;
        title = transform.Find("Title").gameObject;
        BackButtonPress();
    }


    public void StartButtonPress()
    {
        SpeedrunTimer.totalTime = 0;
        SceneManager.LoadScene("Level_1");
    }

    public void SettingsButtonPress()
    {
        HideAll();
        backButton.SetActive(true);
    }

    public void QuitButtonPress()
    {
        Application.Quit();
    }

    public void BackButtonPress()
    {
        HideAll();
        mainMenuButton.SetActive(true);
        title.SetActive(true);
    }

    void HideAll()
    {
        mainMenuButton.SetActive(false);
        backButton.SetActive(false);
        title.SetActive(false);
    }
}
