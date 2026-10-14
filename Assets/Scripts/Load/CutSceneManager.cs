using UnityEngine;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject cutsceneUI;
    [SerializeField] private AudioSource levelAudio;
    [SerializeField] private LevelManager levelManager;

    private void Start()
    {
        cutsceneUI.SetActive(true);

        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.prepareCompleted += OnVideoPrepared;

        videoPlayer.Prepare();
    }

    private void OnVideoFinished(VideoPlayer player)
    {
        // Hide the video and reveal the level.
        cutsceneUI.SetActive(false);
        levelAudio.Play();
        levelManager.levelStarted = true;

        videoPlayer.loopPointReached -= OnVideoFinished;
        videoPlayer.prepareCompleted -= OnVideoPrepared;
    }

    private void OnVideoPrepared(VideoPlayer player)
    {
        player.Play();
    }
}
