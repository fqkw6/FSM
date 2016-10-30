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
            gameObject.AddComponent<DialogBoxController>();
            var dialogBoxController = new DialogBoxController();
            //var dialogBoxController = CreateInstance("DialogBoxController") as DialogBoxController;
            dialogBoxController.DisplayMessage("No Value Entered", "The input cannot be blank.");

            return;
        }

        _gameController.StartNewGame(enteredPlayerName);
    }

    public void ReturnToTitleScreen()
    {
        _gameController.GoToBaseScreen(OutGameScreen.Title);
    }
}
