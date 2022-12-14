using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject pauseMenuUI;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public static void PostProcessingOff()
    {
        PlayerCamera.BloomOnOff(false);
        PlayerCamera.ChromaticAberrationOnOff(false);
        PlayerCamera.vignetteOnOff(false);
    }

    public static void PostProcessingOn()
    {
        PlayerCamera.BloomOnOff(true);
        PlayerCamera.ChromaticAberrationOnOff(true);
        PlayerCamera.vignetteOnOff(true);
    }
}
