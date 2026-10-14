using UnityEngine;
using UnityEngine.UI;

public class FadeImge : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private FadeText text;
    [SerializeField] private float fadeTimeMax = 3f;
    float fadeTimer = 0f;
    public bool fadeIn = false;
    public bool fadeOut = false;

    
    void Start()
    {
        image.gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        image.gameObject.SetActive(true);
        fadeIn = true;
        Color c = image.color;
        c.a = 0f; 
        image.color = c;
    }

    public void FadeOut()
    {
        image.gameObject.SetActive(true);
        fadeOut = true;
        Color c = image.color;
        c.a = 1f; 
        image.color = c;
    }

    void Update()
    {
        if(fadeOut)
        {
            fadeTimer += Time.deltaTime;
            Color c = image.color;
            c.a = (fadeTimeMax-fadeTimer)/fadeTimeMax; 
            image.color = c;
            if (fadeTimer>=fadeTimeMax) 
            {
                if(text != null) text.FadeOut();
                fadeOut = false;
            }
        }
        if(fadeIn)
        {
            fadeTimer += Time.deltaTime;
            Color c = image.color;
            c.a = fadeTimer/fadeTimeMax; 
            image.color = c;
            if (fadeTimer>=fadeTimeMax) {
                if(text != null) text.FadeIn();
                fadeOut = false;
            }
        }
    }
}
