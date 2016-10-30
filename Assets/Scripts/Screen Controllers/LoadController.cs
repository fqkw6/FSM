using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadController : BaseSaveOrLoadController
{
    override public string GetTitleText()
    {
        return "Load";
    }

    public override void SaveOrLoad()
    {
        Load();
    }

    public void Load()
    {
        var saveGameListItem = GetSelectedSaveGameListItem();

        if (saveGameListItem == null)
        {
            return;
        }

        var selectedGameData = GameDictionary[saveGameListItem];

        if (!GameController.TryToLoad(selectedGameData))
        {
            return;
        }

        GameController.GoToCurrentLoadableScreen();
    }

    protected override void PopulateGameList()
    {
        GameDictionary = new Dictionary<GameObject, GameData>();

        var gameDataList = GameController.GetAllSavedGames();

        foreach (var gameData in gameDataList)
        {
            var saveGameItem = CreateGameItem(gameData.SaveGameName, gameData.LastSaveDate);

            GameDictionary.Add(saveGameItem, gameData);
        }
    }
}