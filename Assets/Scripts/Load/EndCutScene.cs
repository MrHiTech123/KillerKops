using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndCutScene : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject cutsceneUI;
    [SerializeField] private FadeImge image;
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
        // cutsceneUI.SetActive(false);
        image.FadeIn();
        videoPlayer.loopPointReached -= OnVideoFinished;
        videoPlayer.prepareCompleted -= OnVideoPrepared;
    }

    private void OnVideoPrepared(VideoPlayer player)
    {
        player.Play();
    }
}
