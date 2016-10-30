using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : ScreenController
{
	public void OpenLoadScreen()
    {
        _gameController.GoToNewScreen(OutGameScreen.Load);
    }

    public void OpenSaveScreen()
    {
        _gameController.GoToNewScreen(OutGameScreen.Save);
    }

    public void OpenOptionsScreen()
    {
        _gameController.GoToNewScreen(OutGameScreen.Options);
    }

    public void ExitToTitleScreen()
    {
        _gameController.GoToBaseScreen(OutGameScreen.Title);
    }

    public void ReturnToGameScreen()
    {
        _gameController.GoToPreviousScreen();
    }

    internal override void OnScreenAwake()
    {
        
    }
}
