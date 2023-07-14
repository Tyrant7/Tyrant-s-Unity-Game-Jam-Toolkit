using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    #region Singleton

    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject, false);
        }
    }

    #endregion

    public static bool LoadingScene { get; private set; } = false;

    public void LoadMenuScene(string sceneName)
    {
        LoadScene(sceneName);
    }
    public void LoadMenuScene()
    {
        LoadMenuScene("MainMenu");
    }

    // Loads a new scene with a transition
    public void LoadScene(string sceneName)
    {
        if (LoadingScene)
        {
            return;
        }
        LoadingScene = true;

        TransitionHandler.Instance.StartSceneTransition(LoadSceneImmediate, sceneName);
    }

    // Loads a new scene without a transition
    public void LoadSceneImmediate(string sceneName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        ao.completed += ctx =>
        {
            LoadingScene = false;
            TransitionHandler.Instance.FinishSceneTransition();
        };
    }
    public void LoadSceneImmediate(int index)
    {
        LoadSceneImmediate(SceneManager.GetSceneByBuildIndex(index).name);
    }
}
