using System;

[Serializable]
public class GameData : IComparable<GameData>
{
    // Save
    public string SaveGameName;
    public DateTime GameStartDate;
    public DateTime LastSaveDate;
    public string SaveGameFilePath;

    // Game Options
    public bool IsHardcoreModeEnabled;

    // Screen Data
    public Screen CurrentLoadableScreen;
    public Screen LastLoadableScreen;
    public Location CurrentLoadableLocation;
    public Location LastLoadableLocation;

    // Other
    public PlayerGameData PlayerGameData;
    public WorldGameData WorldGameData;

    public GameData()
    {
        PlayerGameData = new PlayerGameData();
        WorldGameData = new WorldGameData();
    }

    public void CopyFrom(GameData otherGameData)
    {
        
        SaveGameName = otherGameData.SaveGameName;
        GameStartDate = otherGameData.GameStartDate;
        LastSaveDate = otherGameData.LastSaveDate;
        SaveGameFilePath = otherGameData.SaveGameFilePath;

        IsHardcoreModeEnabled = otherGameData.IsHardcoreModeEnabled;

        PlayerGameData.CopyFrom(otherGameData.PlayerGameData);
        WorldGameData.CopyFrom(otherGameData.WorldGameData);
    }

    int IComparable<GameData>.CompareTo(GameData other)
    {
        if (LastSaveDate == default(DateTime) && other.LastSaveDate != default(DateTime))
        {
            return 1;
        }

        return -LastSaveDate.CompareTo(other.LastSaveDate);
    }
}
