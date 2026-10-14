using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private Image timer;
    void Update()
    {
        timer.fillAmount = (WorldTimer.Instance.levelTimer - WorldTimer.Instance.currentTimeInSeconds)/WorldTimer.Instance.levelTimer;
    }
}
