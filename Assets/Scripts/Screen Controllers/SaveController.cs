using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SaveController : ScreenController
{
    public Transform SaveGameListContent;
    public GameObject SaveGameListItem;

    private Dictionary<GameObject, GameData> _saveGameDictionary;

    private const string NewGameName = "New Save Game";

    internal override void OnScreenAwake()
    {
        PopulateSaveGameList();
    }

    void RefreshSaveGameList()
    {
        ClearSaveGameItems();
        PopulateSaveGameList();
    }

    void PopulateSaveGameList()
    {
        _saveGameDictionary = new Dictionary<GameObject, GameData>();

        var gameDataList = _gameDataController.GetAllSavedGames();

        var newGameData = new GameData();
        var newSaveGameItem = CreateSaveGameItem(NewGameName, null);
        _saveGameDictionary.Add(newSaveGameItem, newGameData);

        foreach (var gameData in gameDataList)
        {
            var saveGameItem = CreateSaveGameItem(gameData.SaveGameName, gameData.LastSaveDate);

            _saveGameDictionary.Add(saveGameItem, gameData);
        }
    }

    GameObject CreateSaveGameItem(string gameSaveName, DateTime? lastSavedDate)
    {
        var newSaveGameListItem = Instantiate(SaveGameListItem);

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

        newSaveGameListItem.GetComponent<GameSaveItemScript>().SaveScreenController = this;

        newSaveGameListItem.transform.SetParent(SaveGameListContent, false);

        return newSaveGameListItem;
    }

    void ClearSaveGameItems()
    {
        foreach (Transform child in SaveGameListContent)
        {
            Destroy(child.gameObject);
        }
    }

    public void Save()
    {
        var saveGameListItem = GetSelectedSaveGameListItem();

        if (saveGameListItem == null)
        {
            return;
        }

        var selectedGameData = _saveGameDictionary[saveGameListItem];

        Debug.Log("Get unique game save name from user");
        var uniqueSaveGameNameFromUser = "First Game";

        if (!_gameDataController.TryToSave(uniqueSaveGameNameFromUser, selectedGameData))
        {
            return;
        }

        RefreshSaveGameList();
    }

    private GameObject GetSelectedSaveGameListItem()
    {
        if (SaveGameListContent == null)
        {
            throw new NullReferenceException("SaveGameListContent");
        }

        foreach (Transform saveGameListItem in SaveGameListContent)
        {
            var gameSaveItemScript = saveGameListItem.GetComponent<GameSaveItemScript>();

            if (gameSaveItemScript.IsSaveGameSelected)
            {
                EventSystem.current.SetSelectedGameObject(gameSaveItemScript.gameObject);

                return saveGameListItem.gameObject;
            }
        }

        return null;
    }


    public void ReturnToMenu()
    {
        _gameDataController.GoToScreen(Screen.Menu);
    }
}
