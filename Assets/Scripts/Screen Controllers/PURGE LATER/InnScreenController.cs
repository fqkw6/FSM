using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InnScreenController : ScreenController
{
    internal override void OnScreenAwake()
    {
    }

    public void ReturnToMapScreen()
    {
        _gameDataController.GoToScreen(Screen.Map);
    }
}
