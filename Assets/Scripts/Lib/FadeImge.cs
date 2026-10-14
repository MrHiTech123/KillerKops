using UnityEngine;
using UnityEngine.UI;

public class FadeImge : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float fadeTimeMax = 3f;
    float fadeTimer = 0f;
    bool fadeIn = false;
    bool fadeOut = false;

    
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
        fadeIn = true;
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
            
        }
    }
}
