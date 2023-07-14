using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for handling a basic game menu. Includes a pause menu and pause menu functionality.
/// </summary>
public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowHidePauseMenu();
        }
    }

    /// <summary>
    /// Shows the pause menu if it's hidden, and hides it if it's shown.
    /// </summary>
    public void ShowHidePauseMenu()
    {
        if (TimeManager.Paused)
            AudioManager.PlayButtonClick();

        TimeManager.PauseGame(!TimeManager.Paused);
        pauseMenu.SetActive(TimeManager.Paused);

        if (TimeManager.Paused)
            AudioManager.PlayButtonClick();
    }

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    public void ReturnToMenu()
    {
        AudioManager.PlayButtonClick();
        SceneLoader.Instance.LoadMenuScene();
    }
}
