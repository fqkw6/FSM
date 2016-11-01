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
            var dialogBoxController = ScriptableObject.CreateInstance("DialogBoxController") as DialogBoxController;

            dialogBoxController.DisplayMessage("Name is Blank", "Your character name cannot be blank.");

            return;
        }

        _gameController.StartNewGame(enteredPlayerName);
    }

    public void ReturnToTitleScreen()
    {
        _gameController.GoToBaseScreen(OutGameScreen.Title);
    }
}
