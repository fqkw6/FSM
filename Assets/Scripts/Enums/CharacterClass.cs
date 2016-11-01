using UnityEngine;
using System.Collections;
using System;

public enum CharacterClass
{
    Client,
    Guard,
    Slave
}

public static class ClassExtensions
{
    public static string ToFriendlyString(this CharacterClass theClass)
    {
        switch (theClass)
        {
            case CharacterClass.Client:
                return "Client";
            case CharacterClass.Guard:
                return "Guard";
            case CharacterClass.Slave:
                return "Slave";
            default:
                throw new Exception("CharacterClass not setup for ToFriendlyString");
        }
    }
}