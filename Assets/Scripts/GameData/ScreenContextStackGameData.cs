using System;
using System.Collections.Generic;

[Serializable]
public class ScreenContextStackGameData
{
    public List<ScreenContextGameData> ScreenContextGameDataList;

    // If has OutGameScreen, show that, since it kinda layers on top. Once that is closed, switch back to the InGameScreen.

    public void Add(ScreenContextGameData screenContextGameData)
    {
        ScreenContextGameDataList.Add(screenContextGameData);
    }

    public void Add(OutGameScreen outGameScreen, InGameScreen inGameScreen)
    {
        var newScreenContextGameData = new ScreenContextGameData(outGameScreen, inGameScreen);

        ScreenContextGameDataList.Add(newScreenContextGameData);
    }

    public void Set(ScreenContextGameData screenContextGameData)
    {
        ScreenContextGameDataList.Clear();
        ScreenContextGameDataList.Add(screenContextGameData);
    }

    public void Set(OutGameScreen outGameScreen, InGameScreen inGameScreen)
    {
        ScreenContextGameDataList.Clear();

        var newScreenContextGameData = new ScreenContextGameData(outGameScreen, inGameScreen);

        ScreenContextGameDataList.Add(newScreenContextGameData);
    }

    public void RemoveLast()
    {
        ScreenContextGameDataList.RemoveAt(ScreenContextGameDataList.Count - 1);
    }

    public bool HasPreviousScreen()
    {
        return ScreenContextGameDataList.Count > 1;
    }

    public bool IsSceneSetup()
    {
        return (ScreenContextGameDataList.Count > 0);
    }

    public ScreenContextGameData GetCurrentScreenContext()
    {
        var lastScreenIndex = ScreenContextGameDataList.Count - 1;

        if (lastScreenIndex < 0)
        {
            throw new Exception("GetCurrentScreenContext failed because ScreenContextGameDataList has no items in list yet.");
        }

        return ScreenContextGameDataList[lastScreenIndex];
    }

    public void CopyFrom(ScreenContextStackGameData otherScreenContextStackGameData)
    {
        ScreenContextGameDataList = new List<ScreenContextGameData>();

        foreach (var item in otherScreenContextStackGameData.ScreenContextGameDataList)
        {
            ScreenContextGameDataList.Add(item);
        }
    }
}