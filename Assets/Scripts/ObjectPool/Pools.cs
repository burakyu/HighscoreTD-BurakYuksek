﻿using System;
using UnityEngine;

public class Pools : MonoBehaviour
{
    public enum Types
    {
        EnemyDefault,
        EnemyTiny,
        EnemyTroll
    }

    public static string GetTypeStr(Types poolType)
    {
        return Enum.GetName(typeof(Types), poolType);
    }
}