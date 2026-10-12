using UnityEngine;

public class WorldTimer : MonoBehaviour
{
    public float currentTimeInSeconds = 0f;
    public float levelTimer = 60f;
    public int nextScene = 1;

    void Update()
    {
        currentTimeInSeconds += Time.deltaTime;
        if(currentTimeInSeconds >= levelTimer)
        {
            Loader.Load((Loader.Scene)nextScene);
        }
    }
}
