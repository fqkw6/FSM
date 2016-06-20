using UnityEngine;
using System.Collections;
using System;

public enum Class
{
    Client,
    Guard,
    Slave
}

public static class ClassExtensions
{
    public static string ToFriendlyString(this Class theClass)
    {
        switch (theClass)
        {
            case Class.Client:
                return "Client";
            case Class.Guard:
                return "Guard";
            case Class.Slave:
                return "Slave";
            default:
                throw new Exception("Class not setup for ToFriendlyString");
        }
    }
}