using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// A simple class which handles basic loading of scenes.
/// Can load scenes immediately, or with a transition.
/// </summary>
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

    [SerializeField] string mainMenu = "MainMenu";
    [SerializeField] string[] levels;

    /// <summary>
    /// Loads the scene whose name corresponds to the mainMenu field of this object.
    /// </summary>
    public void LoadMenuScene()
    {
        LoadScene(mainMenu);
    }

    /// <summary>
    /// Loads the scene whose name corresponds to the string in the levels field at index.
    /// </summary>
    /// <param name="index">The index of the level to load.</param>
    public void LoadLevel(int index)
    {
        LoadScene(levels[index]);
    }

    /// <summary>
    /// Loads a scene with a transition.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void LoadScene(string sceneName)
    {
        if (LoadingScene)
        {
            return;
        }
        LoadingScene = true;

        TransitionHandler.Instance.StartSceneTransition(LoadSceneImmediate, sceneName);
    }
    /// <summary>
    /// Loads a scene with a transition.
    /// </summary>
    /// <param name="index">The index of the scene to load.</param>
    public void LoadScene(int index)
    {
        LoadScene(SceneManager.GetSceneByBuildIndex(index).name);
    }

    /// <summary>
    /// Loads a new scene without a transition.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void LoadSceneImmediate(string sceneName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        ao.completed += ctx =>
        {
            LoadingScene = false;
            TransitionHandler.Instance.FinishSceneTransition();
        };
    }
    /// <summary>
    /// Loads a new scene without a transition.
    /// </summary>
    /// <param name="index">The index of the scene to load.</param>
    public void LoadSceneImmediate(int index)
    {
        LoadSceneImmediate(SceneManager.GetSceneByBuildIndex(index).name);
    }
}
