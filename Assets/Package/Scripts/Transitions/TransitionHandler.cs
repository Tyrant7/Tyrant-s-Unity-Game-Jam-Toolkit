using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// A manager script for transitions. Handles playing transitions, and gives callbacks when they have finished.
/// Good for handling both transitions between scenes and within scenes.
/// </summary>
public class TransitionHandler : MonoBehaviour
{
    #region Singleton

    public static TransitionHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    #endregion

    [SerializeField] GameObject transitionPrefab;
    Animator currentTransitionAnim;
    GameObject transitionInstance = null;

    Action TransitionInCallback;

    Action<string> SceneTransitionCallback;
    string _sceneName;

    public void PlayTransition()
    {
        // If an old one hasn't finished yet, destroy it and replace with the new one
        if (transitionInstance != null)
        {
            Destroy(transitionInstance);
        }

        transitionInstance = Instantiate(transitionPrefab);
        currentTransitionAnim = transitionInstance.GetComponentInChildren<Animator>();

        DontDestroyOnLoad(transitionInstance);

        // Make sure the animation starts at zero and doesn't lag when creating the object
        if (currentTransitionAnim != null)
            currentTransitionAnim.Play("In", -1, 0);
    }

    public void PlayTransition(Action callbackIn)
    {
        TransitionInCallback = callbackIn;
        PlayTransition();
    }

    public void StartSceneTransition(Action<string> finishCallback, string sceneName)
    {
        // IMPORTANT NOTE:
        // Might want to pause here until finished transitioning
        // Depending on type of gameplay

        PlayTransition();

        // Must set here because we don't want it to automatically fade out when loading a new scene
        if (currentTransitionAnim != null)
            currentTransitionAnim.SetBool("CanFadeOut", false);

        SceneTransitionCallback = finishCallback;
        _sceneName = sceneName;
    }

    public void FinishedTransitionIn()
    {
        // CONTINUATION OF NOTE:
        // Would unpause here after scene new is loaded

        if (TransitionInCallback != null)
        {
            TransitionInCallback();
        }

        if (SceneTransitionCallback != null)
        {
            SceneTransitionCallback(_sceneName);
        }

        TransitionInCallback = null;
        SceneTransitionCallback = null;
    }

    public void FinishSceneTransition()
    {
        if (currentTransitionAnim != null)
        {
            currentTransitionAnim.SetBool("CanFadeOut", true);
        }
    }

    public void FinishedTransitionOut()
    {
        if (transitionInstance != null)
        {
            Destroy(transitionInstance);
        }
    }
}
