using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    [Header("Scene Index (Build Profiles)")]
    [SerializeField] private int mainMenuIndex = 0;

    // Load scene berdasarkan index
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Shortcut ke Main Menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
