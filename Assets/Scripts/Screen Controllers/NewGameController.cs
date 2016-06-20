using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameController : ScreenController
{
    public InputField NameInputField;

    internal override void OnScreenAwake()
    {
    }

    public void StartNewGame()
    {
        var enteredPlayerName = NameInputField.text.Trim();

        if (string.IsNullOrEmpty(enteredPlayerName))
        {
            Debug.Log("Name is blank.");

            return;
        }

        _gameDataController.StartNewGame(enteredPlayerName);
    }

    public void ReturnToTitleScreen()
    {
        _gameDataController.GoToScreen(Screen.Title);
    }
}
