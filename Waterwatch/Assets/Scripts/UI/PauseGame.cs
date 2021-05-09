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
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Time.timeScale = 0;
                paused = false;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Time.timeScale = 1;
                paused = true;
                Cursor.visible = false;
                pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }

        }
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        paused = true;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
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
