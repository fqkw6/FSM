using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseSaveOrLoadController : ScriptableObject
{
    public GameDataController GameDataController { get; set; }
    public Transform GameListContent { get; set; }
    public GameObject GameListItem { get; set; }
    public Dictionary<GameObject, GameData> GameDictionary { get; set; }

    public virtual void OnScreenAwake()
    {
        UserPromptForConfirmationController.Initialize();
        UserPromptForStringController.Initialize();

        PopulateGameList();
    }

    public virtual string GetTitleText()
    {
        throw new Exception("GetTitleText not implemented");
    }

    public virtual void SaveOrLoad()
    {
        throw new Exception("SaveOrLoad not implemented");
    }

    protected virtual void PopulateGameList()
    {
        throw new Exception("SaveOrLoad not implemented");
    }

    public void Delete()
    {
        var saveGameListItem = GetSelectedSaveGameListItem();

        if (saveGameListItem == null)
        {
            return;
        }

        var lastSavedText = saveGameListItem.transform.Find("Last Saved Text").GetComponent<Text>();

        if (lastSavedText.text == string.Empty)
        {
            // Can't delete the New Save Game placeholder
            return;
        }

        var saveFileName = saveGameListItem.transform.Find("Game Save Name Text").GetComponent<Text>().text;

        var overwriteMessage = string.Format("Are you sure you want to delete \"{0}\"?", saveFileName);

        UserPromptForConfirmationController.PromptUserForConfirmation("Delete Save?", overwriteMessage, WhenConfirmationPromptButtonIsClicked);
    }

    void WhenConfirmationPromptButtonIsClicked(DialogResult dialogResult)
    {
        if (dialogResult != DialogResult.Ok)
        {
            return;
        }

        var saveGameListItem = GetSelectedSaveGameListItem();

        if (saveGameListItem == null)
        {
            return;
        }

        var saveFileName = saveGameListItem.transform.Find("Game Save Name Text").GetComponent<Text>().text;

        Debug.Log("Delete Save Game: " + saveFileName);

        //GameDataController.TryToDelete(saveGameNameFromUser, selectedGameData);

        RefreshSaveGameList();
    }

    protected GameObject GetSelectedSaveGameListItem()
    {
        if (GameListContent == null)
        {
            throw new NullReferenceException("SaveGameListContent");
        }

        foreach (Transform saveGameListItem in GameListContent)
        {
            var gameSaveItemScript = saveGameListItem.GetComponent<GameListItemScript>();

            if (gameSaveItemScript.IsSaveGameSelected)
            {
                EventSystem.current.SetSelectedGameObject(gameSaveItemScript.gameObject);

                return saveGameListItem.gameObject;
            }
        }

        return null;
    }

    protected void RefreshSaveGameList()
    {
        ClearGameItems();
        PopulateGameList();
    }

    protected void ClearGameItems()
    {
        foreach (Transform child in GameListContent)
        {
            Destroy(child.gameObject);
        }
    }
}
