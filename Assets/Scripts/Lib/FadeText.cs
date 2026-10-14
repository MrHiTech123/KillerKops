using TMPro;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI image;
    [SerializeField] private float fadeTimeMax = 3f;
    [SerializeField] private FadeText fadeNext;
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
            if (fadeTimer>=fadeTimeMax) {
                fadeOut = false;
                if(fadeNext != null)
                {
                    fadeNext.FadeOut();
                }
            }
        }
        if(fadeIn)
        {
            fadeTimer += Time.deltaTime;
            Color c = image.color;
            c.a = fadeTimer/fadeTimeMax; 
            image.color = c;
            if (fadeTimer>=fadeTimeMax) {
                fadeIn = false;
                if(fadeNext != null)
                {
                    fadeNext.FadeIn();
                }
            }
        }
    }
}
