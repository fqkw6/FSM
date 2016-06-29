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

        UserPromptForStringController.PromptUserForString("Save Game Name", WhenStringPromptButtonIsClicked);
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

        GameDataController.TryToSave(saveGameNameFromUser, selectedGameData);

        RefreshSaveGameList();
    }

    protected override void PopulateGameList()
    {
        GameDictionary = new Dictionary<GameObject, GameData>();

        var gameDataList = GameDataController.GetAllSavedGames();

        var newGameData = new GameData();
        var newSaveGameItem = CreateSaveGameItem(NewGameName, null);
        GameDictionary.Add(newSaveGameItem, newGameData);

        foreach (var gameData in gameDataList)
        {
            var saveGameItem = CreateSaveGameItem(gameData.SaveGameName, gameData.LastSaveDate);

            GameDictionary.Add(saveGameItem, gameData);
        }
    }

    GameObject CreateSaveGameItem(string gameSaveName, DateTime? lastSavedDate)
    {
        var newSaveGameListItem = Instantiate(GameListItem);

        var gameSaveNameText = newSaveGameListItem.transform.Find("Game Save Name Text").GetComponent<Text>();
        var lastSavedText = newSaveGameListItem.transform.Find("Last Saved Text").GetComponent<Text>();

        gameSaveNameText.text = gameSaveName;

        if (lastSavedDate == null)
        {
            lastSavedText.text = "";
        }
        else
        {
            lastSavedText.text = lastSavedDate.ToString();
        }

        newSaveGameListItem.GetComponent<GameListItemScript>().SaveScreenController = this;

        newSaveGameListItem.transform.SetParent(GameListContent, false);

        return newSaveGameListItem;
    }

    
}
