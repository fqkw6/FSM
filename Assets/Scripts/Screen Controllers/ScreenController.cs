using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public abstract class ScreenController : MonoBehaviour {

    internal GameDataController _gameDataController;
    public Screen Screen;
    public Location Location;

    void Awake()
    {
        GetGameDataController();

        //_gameDataController.RememberScene(IsSceneLoadable, Scene, Location);

        OnScreenAwake();
    }

    void GetGameDataController()
    {
        if (_gameDataController == null)
        {
            _gameDataController = GameObject.Find("GameDataController").GetComponent<GameDataController>();

            if (_gameDataController.CurrentScreen == Screen.None)
            {
                _gameDataController.CurrentScreen = Screen;
                _gameDataController.CurrentLocation = Location;
            }
        }
    }

    internal abstract void OnScreenAwake();
}
