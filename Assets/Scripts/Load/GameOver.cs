using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Crowd crowd;

    // Update is called once per frame
    void Update()
    {
        if (crowd.getAmountProtesters() <= 0)
        {
            Loader.Load(Loader.Scene.GameOver);
        }
    }
}
