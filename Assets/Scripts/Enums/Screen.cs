using UnityEngine;
using System.Collections;
using System;

public enum Screen
{
    SetInCode,
    Title,
    NewGame,
    Save,
    Load,
    Menu,
    Options,
    Map,
    InsideLocation,
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
                //return "scene_save_and_load";
                return "scene_load";
            case Screen.Menu:
                return "scene_menu";
            case Screen.Options:
                return "scene_options";
            case Screen.Map:
                return "scene_map";
            case Screen.InsideLocation:
                return "scene_inside_location";
            case Screen.SetInCode:
                throw new Exception("Screen is SetInCode, but hasn't been setup yet.");
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