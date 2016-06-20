using System;

[Serializable]
public class PlayerStatsGameData
{
    public int Charm;
    public int Bartering;

    public void CopyFrom(PlayerStatsGameData otherPlayerStatsGameData)
    {
        Charm = otherPlayerStatsGameData.Charm;
        Bartering = otherPlayerStatsGameData.Bartering;
    }
}
