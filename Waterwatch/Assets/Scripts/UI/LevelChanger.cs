using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    public void FadeToGameOver()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(2);
    }
}
