using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for handling a basic main menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Loads the first level.
    /// </summary>
    public void StartGame()
    {
        AudioManager.PlayButtonClick();
        SceneLoader.Instance.LoadLevel(0);
    }

    /// <summary>
    /// Loads a given level.
    /// </summary>
    /// <param name="index">The index of the level to load.</param>
    public void LoadLevel(int index)
    {
        AudioManager.PlayButtonClick();
        SceneLoader.Instance.LoadLevel(index);
    }

    /// <summary>
    /// Loads the main menu scene. Used if multiple menu scenes.
    /// </summary>
    public void ReturnToMenu()
    {
        AudioManager.PlayButtonClick();
        SceneLoader.Instance.LoadMenuScene();
    }

    /// <summary>
    /// Quits the game, if not in the editor.
    /// </summary>
    public void QuitGame()
    {
        if (Application.isEditor)
        {
            return;
        }
        Application.Quit();
    }
}
