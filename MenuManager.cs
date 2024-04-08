using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject ChangelogCanvas;

    bool changelogOpen = false;

    public void StartGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void ChangelogInteract()
    {
        if (!changelogOpen)
        {
            changelogOpen = true;
            ChangelogCanvas.SetActive(true);
        }
        else
        {
            changelogOpen = false;
            ChangelogCanvas.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
