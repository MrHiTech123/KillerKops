using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;

public class CrowdShout : MonoBehaviour
{
    [SerializeField] private Transform[] lines;
    [SerializeField] private RectTransform shoutCanvas;
    FadeText fadeText = null;

    float timer = 0;
    float timerMax;

    float lifeTimer = 0;
    float maxLife = 2f;

    int index = 0;

    void Start()
    {
        timerMax = Random.Range(4f,7f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(timer+" "+ timerMax);
        if(timer >= timerMax)
        {
            Debug.Log("here");
            Transform line = Instantiate(lines[index],shoutCanvas, false);
            line.localPosition = new Vector2(Random.Range(-100f, 100f),Random.Range(50f, 150f));
            fadeText = line.GetComponent<FadeText>();
            timer = 0;
            timerMax = Random.Range(4f,7f);
            index++;
            if(index >= lines.Length)
            {
                index = Random.Range(0,lines.Length);
            }
        }

        if(fadeText != null)
        {
            lifeTimer += Time.deltaTime;
            if(lifeTimer >= maxLife)
            {
                fadeText.FadeOut();
                lifeTimer = 0;
                fadeText = null;
            }
        }
    }

}
