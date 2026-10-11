using UnityEngine;

public class WorldTimer : MonoBehaviour
{
    public float currentTimeInSeconds = 0f;
    private float maxTime = 60f;

    void Update()
    {
        currentTimeInSeconds += Time.deltaTime;
    }
}
