using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameDataController : MonoBehaviour
{
    private static GameDataController _gameDataController;

    public GameData GameData;

    private const string FileNameExtension = ".dat";
    private const string SaveGameSubdirectoryName = "SaveGames";

    public GameDataController()
    {
        GameData = new GameData();
    }

	void Awake ()
    {
	    if (_gameDataController != null)
        {
            Destroy(gameObject);
            return;
        }
        
        _gameDataController = this;
        DontDestroyOnLoad(gameObject);
        GameData = new GameData();
	}

    public void SetupDataForNewGame(string playerName)
    {
        GameData = new GameData();

        _gameDataController.GameData.PlayerGameData.Name = playerName;
    }

    public List<GameData> GetAllSavedGames()
    {
        var allSavedGames = new List<GameData>();

        var saveDirectory = GetSaveDirectory();

        var saveFiles = Directory.GetFiles(saveDirectory, "*" + FileNameExtension);

        foreach (var saveFile in saveFiles)
        {
            Debug.Log("Found save game: " + saveFile);

            var loadedGameData = Load(saveFile);

            if (loadedGameData != null)
            {
                allSavedGames.Add(loadedGameData);
            }
        }

        allSavedGames.Sort();

        // sort by last saved date

        return allSavedGames;
    }

    public bool TryToSave(string saveGameNameFromUser, GameData selectedGameData)
    {
        // keep track of old name so that you can delete it after the current game has been saved
        var oldSaveGameFilePath = _gameDataController.GameData.SaveGameFilePath;
        var oldSaveGameName = _gameDataController.GameData.SaveGameName;
        var oldSaveGameLastSaveDate = _gameDataController.GameData.LastSaveDate;

        var isNewGame = selectedGameData.HasNeverBeenSaved();
        var isSavingUnderNewName = (saveGameNameFromUser != selectedGameData.SaveGameName);
        var needsNewSaveFile = (isNewGame || isSavingUnderNewName);

        Debug.Log(string.Format("isNewGame = {0}, isSavingUnderNewName = {1}, needsNewSaveFile = {2}", isNewGame, isSavingUnderNewName, needsNewSaveFile));

        _gameDataController.GameData.SaveGameName = saveGameNameFromUser;
        _gameDataController.GameData.LastSaveDate = DateTime.Now;

        if (!Save(needsNewSaveFile, selectedGameData.SaveGameFilePath))
        {
            Debug.Log("saving failed");

            _gameDataController.GameData.SaveGameFilePath = oldSaveGameFilePath;
            _gameDataController.GameData.SaveGameName = oldSaveGameName;
            _gameDataController.GameData.LastSaveDate = oldSaveGameLastSaveDate;

            return false;
        }

        var shouldDeleteOldFile = (!isNewGame && isSavingUnderNewName);

        if (shouldDeleteOldFile)
        {
            File.Delete(oldSaveGameFilePath);
        }

        //selectedGameData.CopyFrom(_gameDataController.GameData);

        return true;
    }

    public bool IsSceneSetup()
    {
        return GameData.ScreenContextStackGameData.IsSceneSetup();
    }

    internal void SetBaseScreenAs(ScreenContextGameData screenContextGameData)
    {
        GameData.ScreenContextStackGameData.Set(screenContextGameData);
    }

    internal void SetBaseScreenAs(OutGameScreen newOutGameScreen)
    {
        GameData.ScreenContextStackGameData.Set(newOutGameScreen, InGameScreen.None);
    }

    internal void SetBaseScreenAs(InGameScreen newInGameScreen)
    {
        GameData.ScreenContextStackGameData.Set(OutGameScreen.None, newInGameScreen);
    }

    internal void SetNewScreenAs(OutGameScreen newOutGameScreen)
    {
        GameData.ScreenContextStackGameData.Add(newOutGameScreen, InGameScreen.None);
    }

    internal void SetNewScreenAs(InGameScreen newInGameScreen)
    {
        GameData.ScreenContextStackGameData.Add(OutGameScreen.None, newInGameScreen);
    }

    internal OutGameScreen GetCurrentOutGameScreen()
    {
        var screenContext = GameData.ScreenContextStackGameData.GetCurrentScreenContext();

        return screenContext.OutGameScreen;
    }

    internal InGameScreen GetCurrentInGameScreen()
    {
        var screenContext = GameData.ScreenContextStackGameData.GetCurrentScreenContext();

        return screenContext.InGameScreen;
    }

    internal string GetCurrentSceneName()
    {
        var screenContext = GameData.ScreenContextStackGameData.GetCurrentScreenContext();

        return screenContext.GetCurrentSceneName();
    }

    internal string GetLoadableSceneName()
    {
        var screenContext = GameData.ScreenContextStackGameData.GetCurrentScreenContext();

        return screenContext.GetLoadableSceneName();
    }

    internal bool HasPreviousScreen()
    {
        return GameData.ScreenContextStackGameData.HasPreviousScreen();
    }

    internal void SetScreenAsPrevious()
    {
        GameData.ScreenContextStackGameData.RemoveLast();
    }

    private bool Save(bool needsNewSaveFile, string selectedSaveGamePath)
    {
        string filePath;

        if (needsNewSaveFile)
        {
            filePath = GetNewFilePath();

            _gameDataController.GameData.SaveGameFilePath = filePath;
        }
        else
        {
            _gameDataController.GameData.SaveGameFilePath = selectedSaveGamePath;
            filePath = _gameDataController.GameData.SaveGameFilePath;
        }

        Debug.Log("save file path = " + filePath);

        try
        {
            using (var fileStream = File.Create(filePath))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, _gameDataController.GameData);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);

            return false;
        }

        return true;
    }

    private string GetNewFilePath(string firstNameAttempt = null, int attemptNumber = 1)
    {
        if (firstNameAttempt == null)
        {
            firstNameAttempt = _gameDataController.GameData.SaveGameName;

            foreach (var c in Path.GetInvalidFileNameChars())
            {
                firstNameAttempt = firstNameAttempt.Replace(c, '-');
            }
        }

        var possibleFileName = firstNameAttempt;

        if (attemptNumber > 1)
        {
            possibleFileName += attemptNumber;
        }

        possibleFileName += FileNameExtension;

        var saveDirectory = GetSaveDirectory();

        var possibleNewFilePath = Path.Combine(saveDirectory, possibleFileName);

        if (File.Exists(possibleNewFilePath))
        {
            return GetNewFilePath(firstNameAttempt, attemptNumber + 1);
        }

        return possibleNewFilePath;
    }

    private string GetSaveDirectory()
    {
        var saveDirectory = Path.Combine(Application.persistentDataPath, SaveGameSubdirectoryName);

        Directory.CreateDirectory(saveDirectory);

        return saveDirectory;
    }

    public void TryToDelete(GameData selectedGameData)
    {
        var filePath = selectedGameData.SaveGameFilePath;

        Debug.Log("DELETE FILE THAT ISN'T USED ANYMORE: " + filePath);
        File.Delete(filePath);
    }

    public bool TryToLoad(GameData gameData)
    {
        _gameDataController.GameData = gameData;

        return true;
    }

    private GameData Load(string filePath)
    {
        try
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                var binaryFormatter = new BinaryFormatter();

                return (GameData)binaryFormatter.Deserialize(fileStream);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        return null;
    }
}