using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Drag Main Camera here
    public string nextSceneName = "Scene_01_Game"; // Your actual gameplay scene

    void Start()
    {
        // Subscribe to the "loopPointReached" event (End of video)
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        // Allow user to skip with Spacebar (Optional, but good for testing)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextScene();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}