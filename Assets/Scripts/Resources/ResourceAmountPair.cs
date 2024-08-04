using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceAmountPair
{
    public ResourceType collectableType;

    public int amount;

    public ResourceAmountPair(ResourceType collectableType, int amount)
    {
        this.collectableType = collectableType;
        this.amount = amount;
    }
}
