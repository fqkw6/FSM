using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : ScreenController
{
	public void OpenNewGameScreen()
    {
        _gameController.GoToNewScreen(OutGameScreen.NewGame);
    }

    public void OpenLoadGameScreen()
    {
        _gameController.GoToNewScreen(OutGameScreen.Load);
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
