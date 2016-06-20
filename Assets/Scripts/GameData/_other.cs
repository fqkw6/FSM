using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Character
{
    public string Name;
    public Sex Sex;
    public Class Class;
    public Stats Stats;
}

[Serializable]
public class Stats
{
    public int Deception;
    public int Enjoyment;
    public int Morale;
}

[Serializable]
public class SlaveStats : Stats
{
    //public string Deception;
    //public string Enjoyment;
    //public string Morale;
    public string SlaveStat;
}