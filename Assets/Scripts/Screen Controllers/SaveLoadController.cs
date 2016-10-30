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
        //Screen = ; ..CurrentScreen;

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

        _saveOrLoadController.GameController = _gameController;
        _saveOrLoadController.GameListContent = GameListContent;
        _saveOrLoadController.GameListItem = GameListItem;
    }

    private bool IsSaveGameScreen()
    {
        var gameScreen = _gameController.GameDataController.GetCurrentOutGameScreen();

        if (gameScreen == OutGameScreen.Save)
        {
            return true;
        }

        if (gameScreen == OutGameScreen.Load)
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
        Debug.Log("SaveLoadController.SaveOrLoad()");
        _saveOrLoadController.SaveOrLoad();
    }

    public void Delete()
    {
        _saveOrLoadController.Delete();
    }

    public void ReturnToPriorScreen()
    {
        _gameController.GoToPreviousScreen();
    }
}
