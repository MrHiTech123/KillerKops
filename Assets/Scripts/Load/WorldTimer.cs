using UnityEngine;

public class WorldTimer : MonoBehaviour
{
    public static WorldTimer Instance {get; private set;} 
    public float currentTimeInSeconds = 0f;
    public float levelTimer = 60f;
    public int nextScene = 1;
    public bool levelStarted = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(levelStarted){
            currentTimeInSeconds += Time.deltaTime;
            if(currentTimeInSeconds >= levelTimer)
            {
                Loader.Load((Loader.Scene)nextScene);
            }
        }
    }
}
