using UnityEngine;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject cutsceneUI;
    [SerializeField] private AudioSource levelAudio;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private WorldTimer worldTimer;

    private void Start()
    {
        cutsceneUI.SetActive(true);

        videoPlayer.loopPointReached += OnVideoFinished;
        

        videoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer player)
    {
        // Hide the video and reveal the level.
        cutsceneUI.SetActive(false);
        levelAudio.Play();
        levelManager.levelStarted = true;
        worldTimer.levelStarted = true;

        videoPlayer.loopPointReached -= OnVideoFinished;
    }

    
}
