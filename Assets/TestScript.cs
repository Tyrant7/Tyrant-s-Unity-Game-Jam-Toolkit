using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    #region Singleton

    public static TestScript Instance { get; private set; }

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

    [ContextMenu("Transition Between Scenes")]
    public void TransitionBetweenScenes()
    {
        SceneLoader.Instance.LoadScene("SceneA");
    }

    [ContextMenu("Transition On Same Scene")]
    public void TransitionOnSameScene()
    {
        TransitionHandler.Instance.PlayTransition(() =>
        {
            Debug.Log("Done");
        });
    }

    [ContextMenu("Small shake")]
    public void SmallShake()
    {
        CinemachineShake.Instance.ShakeCamera(0.5f, 0.15f, 0.5f);
    }

    [ContextMenu("Big shake")]
    public void BigShake()
    {
        CinemachineShake.Instance.ShakeCamera(1.5f, 0.25f, 1.2f);
    }

    bool onTrackA = true;
    [SerializeField] AudioClip clipA, clipB;

    [ContextMenu("Crossfade")]
    public void Crossfade()
    {
        if (onTrackA)
            SoundtrackManager.Instance.PlaySoundtrack(clipB, true);
        else
            SoundtrackManager.Instance.PlaySoundtrack(clipA, true);
        onTrackA = !onTrackA;
    }
}
