using UnityEngine;
using UnityEngine.UI;

public class ReturnBack : MonoBehaviour
{
    [SerializeField] private Button button;
    void Start()
    {
        // Register the listener
        button.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        Loader.Load(Loader.Scene.Level_1);
    }
}
