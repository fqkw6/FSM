using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SaveLoadController : ScreenController
{
    public Transform GameListContent;
    public GameObject GameListItem;
    public Text SaveOrLoadTitleText;
    public Text SaveOrLoadButtonText;

    private BaseSaveOrLoadController _saveOrLoadController;

    internal override void OnScreenAwake()
    {
        Screen = _gameDataController.CurrentScreen;

        SetupController();

        SetupUI();

        _saveOrLoadController.OnScreenAwake();
    }

    private void SetupController()
    {
        if (IsSaveGameScreen())
        {
            _saveOrLoadController = ScriptableObject.CreateInstance<SaveController>();
        }
        else
        {
            _saveOrLoadController = ScriptableObject.CreateInstance<LoadController>();
        }

        _saveOrLoadController.GameDataController = _gameDataController;
        _saveOrLoadController.GameListContent = GameListContent;
        _saveOrLoadController.GameListItem = GameListItem;
    }

    private bool IsSaveGameScreen()
    {
        if (Screen == Screen.Save)
        {
            return true;
        }

        if (Screen == Screen.Load)
        {
            return false;
        }

        throw new Exception("Current screen isn't save or load.");
    }

    private void SetupUI()
    {
        SaveOrLoadTitleText.text = _saveOrLoadController.GetTitleText();
        SaveOrLoadButtonText.text = _saveOrLoadController.GetTitleText();
    }

    public void SaveOrLoad()
    {
        _saveOrLoadController.SaveOrLoad();
    }

}
