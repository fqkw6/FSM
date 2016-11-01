using System;

[Serializable]
public class ScreenContextGameData
{
    public OutGameScreen OutGameScreen;
    public InGameScreen InGameScreen;

    // If has OutGameScreen, show that, since it kinda layers on top. Once that is closed, switch back to the InGameScreen.

    public ScreenContextGameData(OutGameScreen outGameScreen, InGameScreen inGameScreen)
    {
        OutGameScreen = outGameScreen;
        InGameScreen = inGameScreen;
    }

    public bool IsLoadable()
    {
        return InGameScreen != InGameScreen.None;
    }

    public string GetCurrentSceneName()
    {
        if (HasOutGameScreen())
        {
            switch (OutGameScreen)
            {
                case OutGameScreen.Title:
                    return "scene_title";
                case OutGameScreen.NewGame:
                    return "scene_new_game";
                case OutGameScreen.Save:
                case OutGameScreen.Load:
                    return "scene_save_and_load";
                case OutGameScreen.Menu:
                    return "scene_menu";
                case OutGameScreen.Options:
                    return "scene_options";
                case OutGameScreen.None:
                    throw new Exception("GetCurrentSceneName failed because OutGameScreen is None.");
                default:
                    throw new Exception("GetCurrentSceneName failed because OutGameScreen isn't in list: " + OutGameScreen);
            }
        }
        else
        {
            return GetLoadableSceneName();
        }
        /*case Screen.SetInCodeLater:
            throw new Exception("Screen is SetInCodeLater, but hasn't been setup yet.");*/
    }

    public string GetLoadableSceneName()
    {
        switch (InGameScreen)
        {
            case InGameScreen.Map:
                return "scene_map";
            case InGameScreen.EstateEntrance:
            case InGameScreen.SlaveHouseEntrance:
            case InGameScreen.InnEntrance:
                return "scene_entrance";
            case InGameScreen.SlaveHouseBrowsingSlaves:
            case InGameScreen.InnBrowsingClients:
                return "scene_person_navigator";
            case InGameScreen.None:
                throw new Exception("GetCurrentSceneName failed because InGameScreen is None.");
            default:
                throw new Exception("GetCurrentSceneName failed because InGameScreen isn't in list: " + InGameScreen);
        }
    }

    private bool HasOutGameScreen()
    {
        return (OutGameScreen != OutGameScreen.None);
    }

    public void CopyFrom(ScreenContextGameData otherScreenContextGameData)
    {
        OutGameScreen = otherScreenContextGameData.OutGameScreen;
        InGameScreen = otherScreenContextGameData.InGameScreen;
    }
}