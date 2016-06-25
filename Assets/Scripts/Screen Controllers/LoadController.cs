using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadController : BaseSaveOrLoadController
{
    override public void OnScreenAwake()
    {
        PopulateLoadGameList();
    }

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

        if (!GameDataController.TryToLoad(selectedGameData))
        {
            return;
        }

        var sceneToLoad = GameDataController.GameData.LastLoadableScreen.GetSceneName();

        SceneManager.LoadScene(sceneToLoad);
    }

    void PopulateLoadGameList()
    {
        GameDictionary = new Dictionary<GameObject, GameData>();

        var gameDataList = GameDataController.GetAllSavedGames();

        foreach (var gameData in gameDataList)
        {
            var saveGameItem = CreateLoadGameItem(gameData.SaveGameName, gameData.LastSaveDate);

            GameDictionary.Add(saveGameItem, gameData);
        }
    }

    GameObject CreateLoadGameItem(string gameSaveName, DateTime? lastSavedDate)
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

        newSaveGameListItem.GetComponent<GameListItemScript>().LoadScreenController = this;

        newSaveGameListItem.transform.SetParent(GameListContent, false);

        return newSaveGameListItem;
    }

    private GameObject GetSelectedSaveGameListItem()
    {
        if (GameListContent == null)
        {
            throw new NullReferenceException("SaveGameListContent");
        }

        foreach (Transform loadGameListItem in GameListContent)
        {
            var gameLoadItemScript = loadGameListItem.GetComponent<GameListItemScript>();

            if (gameLoadItemScript.IsSaveGameSelected)
            {
                EventSystem.current.SetSelectedGameObject(gameLoadItemScript.gameObject);

                return loadGameListItem.gameObject;
            }
        }

        return null;
    }
}