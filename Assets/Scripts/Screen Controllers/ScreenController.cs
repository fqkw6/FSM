using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public abstract class ScreenController : MonoBehaviour {

    internal GameController _gameController;
    public ScreenContextGameData ScreenContextGameData;

    void Awake()
    {
        GetGameDataController();

        //_gameDataController.RememberScene(IsSceneLoadable, Scene, Location);

        OnScreenAwake();
    }

    void GetGameDataController()
    {
        if (_gameController == null)
        {
            _gameController = GameObject.Find("GameController").GetComponent<GameController>();

            if (!_gameController.IsSceneSetup())
            {
                _gameController.SetupBaseScreen(ScreenContextGameData);
            }
        }
    }

    internal abstract void OnScreenAwake();
}
