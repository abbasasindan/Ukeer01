using UnityEngine;
using UnityEngine.SceneManagement; // Required to change scenes

public class MainMenu : MonoBehaviour
{
    [Header("Scene Names")]
    // EXACT spelling of your scene names in Build Settings
    public string cutsceneSceneName = "Scene_00_IntroVideo";

    public void PlayGame()
    {
        // Loads the video/trailer scene
        SceneManager.LoadScene(cutsceneSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game requested.");
        Application.Quit();
    }
}