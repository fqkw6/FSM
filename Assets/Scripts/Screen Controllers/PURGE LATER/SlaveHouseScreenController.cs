using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlaveHouseScreenController : ScreenController
{
    internal override void OnScreenAwake()
    {
    }

    public void ReturnToMapScreen()
    {
        _gameController.GoToPreviousScreen();
    }
}
