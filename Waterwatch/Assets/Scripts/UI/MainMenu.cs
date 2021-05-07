using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=YMj2qPq9CP8&ab_channel=Brackeys
public class MainMenu : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider loading;
    public void PlayGame()
    {
        // SceneManager.LoadScene(1);
        StartCoroutine(LoadGameAsync());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadGameAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        LoadingScreen.SetActive(true);

        Cursor.visible = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loading.value = progress;
            yield return null;
        }
    }
}
