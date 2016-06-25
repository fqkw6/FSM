using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseSaveOrLoadController : ScriptableObject
{
    public GameDataController GameDataController { get; set; }
    public Transform GameListContent { get; set; }
    public GameObject GameListItem { get; set; }
    public Dictionary<GameObject, GameData> GameDictionary { get; set; }

    public void ReturnToPriorScreen()
    {
        GameDataController.GoToPreviousScreen();
    }

    public virtual void OnScreenAwake()
    {
        throw new Exception("OnScreenAwake not implemented");
    }

    public virtual string GetTitleText()
    {
        throw new Exception("GetTitleText not implemented");
    }

    public virtual void SaveOrLoad()
    {
        throw new Exception("SaveOrLoad not implemented");
    }
}
