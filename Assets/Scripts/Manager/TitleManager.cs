using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnStart2ButtonClick()
    {
        SceneManager.LoadScene("New Map");
    }

    public void OnUpgradeButtonClick()
    {
        Debug.Log("TODO");
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
