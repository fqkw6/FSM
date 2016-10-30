using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _gameController;

    public GameDataController GameDataController;

    public GameController()
    {
        GameDataController = new GameDataController();
    }

	void Awake ()
    {
	    if (_gameController != null)
        {
            Destroy(gameObject);
            return;
        }
        
        _gameController = this;
        DontDestroyOnLoad(gameObject);
        GameDataController = new GameDataController();
	}

    public void StartNewGame(string playerName)
    {
        GameDataController.SetupDataForNewGame(playerName);

        GoToBaseScreen(InGameScreen.Map);
    }

    public List<GameData> GetAllSavedGames()
    {
        return GameDataController.GetAllSavedGames();
    }

    public bool TryToSave(string saveGameNameFromUser, GameData selectedGameData)
    {
        return GameDataController.TryToSave(saveGameNameFromUser, selectedGameData);
    }

    public void TryToDelete(GameData selectedGameData)
    {
        GameDataController.TryToDelete(selectedGameData);
    }

    public bool TryToLoad(GameData gameData)
    {
        return GameDataController.TryToLoad(gameData);
    }

    public bool IsSceneSetup()
    {
        return GameDataController.IsSceneSetup();
    }

    public void SetupBaseScreen(OutGameScreen newOutGameScreen)
    {
        GameDataController.SetBaseScreenAs(newOutGameScreen);
    }

    public void SetupBaseScreen(ScreenContextGameData screenContextGameData)
    {
        GameDataController.SetBaseScreenAs(screenContextGameData);
    }

    public void GoToBaseScreen(OutGameScreen newOutGameScreen)
    {
        GameDataController.SetBaseScreenAs(newOutGameScreen);
        GoToTheSetupScene();
    }

    public void GoToBaseScreen(InGameScreen newInGameScreen)
    {
        GameDataController.SetBaseScreenAs(newInGameScreen);
        GoToTheSetupScene();
    }

    public void GoToNewScreen(OutGameScreen newOutGameScreen)
    {
        GameDataController.SetNewScreenAs(newOutGameScreen);
        GoToTheSetupScene();
    }

    public void GoToNewScreen(InGameScreen newInGameScreen)
    {
        GameDataController.SetNewScreenAs(newInGameScreen);
        GoToTheSetupScene();
    }

    private void GoToTheSetupScene()
    {
        try
        {
            var sceneName = GameDataController.GetCurrentSceneName();

            SceneManager.LoadScene(sceneName);
        }
        catch (Exception e)
        {
            throw new Exception("Failed to GoToTheSetupScene.", e);
        }
    }

    public void GoToPreviousScreen()
    {
        if (GameDataController.HasPreviousScreen())
        {
            GameDataController.SetScreenAsPrevious();
            GoToTheSetupScene();
        }
    }

    public void GoToCurrentLoadableScreen()
    {
        try
        {
            var sceneName = GameDataController.GetLoadableSceneName();

            SceneManager.LoadScene(sceneName);
        }
        catch (Exception e)
        {
            throw new Exception("Failed to GoToCurrentLoadableScreen.", e);
        }
    }
}