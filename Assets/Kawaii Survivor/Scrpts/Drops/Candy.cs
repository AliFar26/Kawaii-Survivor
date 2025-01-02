using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : DropableCurrency
{

    [Header("Elements")]
    public static Action<Candy> onCollected;
    protected override void Collected()
    {
        onCollected?.Invoke(this);
    }
}
