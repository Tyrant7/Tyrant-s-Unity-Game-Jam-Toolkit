using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class which allows for easy pausing of the game while remembering the previous timeScale value, 
/// for games that use slow motion or multiple methods of pausing.
/// Use for all time related actions.
/// </summary>
public class TimeManager : MonoBehaviour
{
    private static float TimeScale = 1;

    public static bool Paused { get; private set; } = false;

    private void Update()
    {
        if (Paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = TimeScale;
        }
    }

    public static void SetTimeScale(float timeScale)
    {
        TimeScale = timeScale;
    }

    public static void PauseGame(bool paused)
    {
        Paused = paused;
    }
}
