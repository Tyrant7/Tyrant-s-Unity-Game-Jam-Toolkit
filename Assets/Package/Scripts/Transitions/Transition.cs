using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that should be attached to the transition prefab's game object.
/// </summary>
public class Transition : MonoBehaviour
{
    /// <summary>
    /// Should be called from an animation event when the "in" animation has finished.
    /// </summary>
    public void FinishedIn()
    {
        TransitionHandler.Instance.FinishedTransitionIn();
    }

    /// <summary>
    /// Should be called from an animation event when the "out" animation has finished.
    /// </summary>
    public void FinishedOut()
    {
        TransitionHandler.Instance.FinishedTransitionOut();
    }
}
