using System;

[Serializable]
public class PlayerGameData
{
    public string Name;
    public int Gold;
    public PlayerStatsGameData PlayerStats;

    public PlayerGameData()
    {
        PlayerStats = new PlayerStatsGameData();
    }

    public void CopyFrom(PlayerGameData otherPlayerGameData)
    {
        Name = otherPlayerGameData.Name;
        Gold = otherPlayerGameData.Gold;
        PlayerStats.CopyFrom(otherPlayerGameData.PlayerStats);
    }
}