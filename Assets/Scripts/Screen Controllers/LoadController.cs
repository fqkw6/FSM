using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadController : ScreenController
{
    public Transform LoadGameListContent;
    public GameObject LoadGameListItem;

    private Dictionary<GameObject, GameData> _loadGameDictionary;

    internal override void OnScreenAwake()
    {
        PopulateLoadGameList();
    }

    void PopulateLoadGameList()
    {
        _loadGameDictionary = new Dictionary<GameObject, GameData>();

        var gameDataList = _gameDataController.GetAllSavedGames();

        foreach (var gameData in gameDataList)
        {
            var saveGameItem = CreateLoadGameItem(gameData.SaveGameName, gameData.LastSaveDate);

            _loadGameDictionary.Add(saveGameItem, gameData);
        }
    }

    GameObject CreateLoadGameItem(string gameSaveName, DateTime? lastSavedDate)
    {
        var newSaveGameListItem = Instantiate(LoadGameListItem);

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

        newSaveGameListItem.GetComponent<GameSaveItemScript>().LoadScreenController = this;

        newSaveGameListItem.transform.SetParent(LoadGameListContent, false);

        return newSaveGameListItem;
    }

    public void Load()
    {
        var saveGameListItem = GetSelectedSaveGameListItem();

        if (saveGameListItem == null)
        {
            return;
        }

        var selectedGameData = _loadGameDictionary[saveGameListItem];

        if (!_gameDataController.TryToLoad(selectedGameData))
        {
            return;
        }

        var sceneToLoad = _gameDataController.GameData.LastLoadableScreen.GetSceneName();

        SceneManager.LoadScene(sceneToLoad);
    }

    private GameObject GetSelectedSaveGameListItem()
    {
        if (LoadGameListContent == null)
        {
            throw new NullReferenceException("SaveGameListContent");
        }

        foreach (Transform loadGameListItem in LoadGameListContent)
        {
            var gameLoadItemScript = loadGameListItem.GetComponent<GameSaveItemScript>();

            if (gameLoadItemScript.IsSaveGameSelected)
            {
                EventSystem.current.SetSelectedGameObject(gameLoadItemScript.gameObject);

                return loadGameListItem.gameObject;
            }
        }

        return null;
    }

    public void ReturnToPreviousScreen()
    {
        _gameDataController.GoToPreviousScreen();
    }
}