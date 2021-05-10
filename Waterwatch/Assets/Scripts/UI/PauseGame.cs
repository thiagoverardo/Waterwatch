using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseGame : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;
    public GameObject ScrollPanel;
    public HUD hud;
    void Start()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                if (ScrollPanel.activeSelf){
                    hud.CloseScrollPanel();
                }
                Time.timeScale = 0;
                paused = true;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                UnpauseGame();
            }

        }
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
