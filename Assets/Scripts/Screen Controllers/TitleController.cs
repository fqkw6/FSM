using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : ScreenController
{
	public void OpenNewGameScreen()
    {
        _gameDataController.GoToScreen(Screen.NewGame);
    }

    public void OpenLoadGameScreen()
    {
        _gameDataController.GoToScreen(Screen.Load);
    }

    public void ExitGame()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    internal override void OnScreenAwake()
    {
        
    }
}
