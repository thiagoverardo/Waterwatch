using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;

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
