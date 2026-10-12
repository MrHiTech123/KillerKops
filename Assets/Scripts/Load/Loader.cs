using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    private static Scene targetScene;
    public enum Scene
    {
        StartScene,
        Level_1,
        Level_2,
        Level_3,
        LoadingScene,
    }
    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());

    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());

        
    }
}
