using UnityEngine;
using System.Collections;
using System;

public enum CharacterSex
{
    Female,
    Male
}

public static class SexExtensions
{
    public static string ToFriendlyString(this CharacterSex sex)
    {
        switch (sex)
        {
            case CharacterSex.Female:
                return "Female";
            case CharacterSex.Male:
                return "Male";
            default:
                throw new Exception("CharacterSex not setup for ToFriendlyString");
        }
    }
}