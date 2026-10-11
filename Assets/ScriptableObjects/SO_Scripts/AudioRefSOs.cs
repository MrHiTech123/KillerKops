using UnityEngine;

[CreateAssetMenu(fileName = "AudioRefSOs", menuName = "Scriptable Objects/AudioRefSOs")]
public class AudioRefSOs : ScriptableObject
{
    public AudioClip gunFire;
    public AudioClip tearGas;
    public AudioClip glassBreak;
    public AudioClip crowd;
    public AudioClip[] level1Dialog;
    public AudioClip[] level2Dialog;
    public AudioClip[] level3Dialog;    
}
