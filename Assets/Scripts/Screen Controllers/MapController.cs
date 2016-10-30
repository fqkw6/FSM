using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : ScreenController
{
    public Text CharacterNameText;

    internal override void OnScreenAwake()
    {
        CharacterNameText.text = _gameController.GameDataController.GameData.PlayerGameData.Name;
    }

    public void OpenSlaveHouseScreen()
    {
        _gameController.GoToNewScreen(InGameScreen.SlaveHouseEntrance);
    }

    public void OpenInnScreen()
    {
        _gameController.GoToNewScreen(InGameScreen.InnEntrance);
    }

    public void OpenMenuScreen()
    {
        _gameController.GoToNewScreen(OutGameScreen.Menu);
    }
}
