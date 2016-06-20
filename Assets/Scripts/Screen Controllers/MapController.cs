using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : ScreenController
{
    public Text CharacterNameText;

    internal override void OnScreenAwake()
    {
        CharacterNameText.text = _gameDataController.GameData.PlayerGameData.Name;
    }

    public void OpenSlaveHouseScreen()
    {
        _gameDataController.GoToScreen(Screen.InsideLocation, Location.SlaveHouse);
    }

    public void OpenInnScreen()
    {
        _gameDataController.GoToScreen(Screen.InsideLocation, Location.Inn);
    }

    public void OpenMenuScreen()
    {
        _gameDataController.GoToScreen(Screen.Menu);
    }
}
