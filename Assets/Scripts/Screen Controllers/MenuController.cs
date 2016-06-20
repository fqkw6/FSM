using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : ScreenController
{
	public void OpenLoadScreen()
    {
        _gameDataController.GoToScreen(Screen.Load);
    }

    public void OpenSaveScreen()
    {
        _gameDataController.GoToScreen(Screen.Save);
    }

    public void OpenOptionsScreen()
    {
        _gameDataController.GoToScreen(Screen.Options);
    }

    public void ExitToTitleScreen()
    {
        _gameDataController.GoToScreen(Screen.Title);
    }

    public void ReturnToGameScreen()
    {
        _gameDataController.GoToScreen(Screen.Map);
    }

    internal override void OnScreenAwake()
    {
        
    }
}
