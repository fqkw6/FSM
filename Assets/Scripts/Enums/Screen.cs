using UnityEngine;
using System.Collections;
using System;

public enum Screen
{
    None = 0,
    SetInCodeLater = 1,
    Title = 2,
    NewGame = 3,
    Save = 4,
    Load = 5,
    Menu = 6,
    Options = 7,
    Map = 8,
    InsideLocation = 9
}

public static class ScreenExtensions
{
    public static string GetSceneName(this Screen screen)
    {
        switch (screen)
        {
            case Screen.Title:
                return "scene_title";
            case Screen.NewGame:
                return "scene_new_game";
            case Screen.Save:
            case Screen.Load:
                return "scene_save_and_load";
            case Screen.Menu:
                return "scene_menu";
            case Screen.Options:
                return "scene_options";
            case Screen.Map:
                return "scene_map";
            case Screen.InsideLocation:
                return "scene_inside_location";
            case Screen.None:
                throw new Exception("Screen is None.");
            case Screen.SetInCodeLater:
                throw new Exception("Screen is SetInCodeLater, but hasn't been setup yet.");
            default:
                throw new Exception("Screen not setup for GetScreenName");
        }
    }

    public static bool CanBeLoadedTo(this Screen screen)
    {
        switch (screen)
        {
            case Screen.Map:
            case Screen.InsideLocation:
                return true;
            default:
                return false;
        }
    }
}