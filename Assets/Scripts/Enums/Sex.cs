using UnityEngine;
using System.Collections;
using System;

public enum Sex
{
    Female,
    Male
}

public static class SexExtensions
{
    public static string ToFriendlyString(this Sex sex)
    {
        switch (sex)
        {
            case Sex.Female:
                return "Female";
            case Sex.Male:
                return "Male";
            default:
                throw new Exception("Sex not setup for ToFriendlyString");
        }
    }
}