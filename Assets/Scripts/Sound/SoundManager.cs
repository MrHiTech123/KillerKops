using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; private set;}

    [SerializeField] private AudioRefSOs audioClips;
    private float volume = 1f;

    private void Awake()
    {
        Instance = this;
    }

    public void GunShot(Vector3 position)
    {
        PlaySound(audioClips.gunFire, position, .5f);
    }

    public void TearGas(Vector3 position)
    {
        PlaySound(audioClips.tearGas, position, 1f);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    
}
