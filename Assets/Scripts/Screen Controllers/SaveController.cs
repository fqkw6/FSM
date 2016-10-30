using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SaveController : BaseSaveOrLoadController
{
    private const string NewGameName = "New Save Game";

    override public string GetTitleText()
    {
        return "Save";
    }

    public override void SaveOrLoad()
    {
        Save();
    }

    public void Save()
    {
        var saveGameListItem = GetSelectedSaveGameListItem();

        if (saveGameListItem == null)
        {
            return;
        }

        var selectedGameData = GameDictionary[saveGameListItem];

        string prepopulateSaveGameName = string.Empty;

        if (!selectedGameData.HasNeverBeenSaved())
        {
            prepopulateSaveGameName = selectedGameData.SaveGameName;
        }

        var dialogBoxController = CreateInstance("DialogBoxController") as DialogBoxController;
        dialogBoxController.PromptUserForString("Save Game Name", prepopulateSaveGameName, WhenStringPromptButtonIsClicked);
    }

    void WhenStringPromptButtonIsClicked(DialogResult dialogResult, string saveGameNameFromUser)
    {
        if (dialogResult != DialogResult.Ok)
        {
            return;
        }

        Debug.Log("Game Name = " + saveGameNameFromUser);

        var saveGameListItem = GetSelectedSaveGameListItem();

        if (saveGameListItem == null)
        {
            return;
        }

        var selectedGameData = GameDictionary[saveGameListItem];

        GameController.TryToSave(saveGameNameFromUser, selectedGameData);

        RefreshSaveGameList();
    }

    protected override void PopulateGameList()
    {
        GameDictionary = new Dictionary<GameObject, GameData>();

        var gameDataList = GameController.GetAllSavedGames();

        var newGameData = new GameData();
        var newSaveGameItem = CreateGameItem(NewGameName, null);
        GameDictionary.Add(newSaveGameItem, newGameData);

        foreach (var gameData in gameDataList)
        {
            var saveGameItem = CreateGameItem(gameData.SaveGameName, gameData.LastSaveDate);

            GameDictionary.Add(saveGameItem, gameData);
        }
    }

    
}
