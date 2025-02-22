using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoIntroManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Button startButton; 
    [SerializeField] private Button skipButton;  
    [SerializeField] private string gameSceneName = "Level_1"; 

    private bool isVideoPlaying = false; 

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartVideo);
        skipButton.onClick.AddListener(SkipVideo);
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartVideo);
        skipButton.onClick.RemoveListener(SkipVideo);
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

    void Update()
    {
        if (isVideoPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            SkipVideo();
        }
    }

    private void StartVideo()
    {
        isVideoPlaying = true;
        videoPlayer.Play();
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
