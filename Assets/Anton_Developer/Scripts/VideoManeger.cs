using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string gameSceneName = "Level_2";

    private bool isVideoPlaying = false;

    private void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        isVideoPlaying = true;
    }

    private void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

    void Update()
    {
        if (isVideoPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            SkipVideo();
        }
    }

    private void SkipVideo()
    {
        if (isVideoPlaying)
        {
            isVideoPlaying = false;
            LoadGameScene();
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
