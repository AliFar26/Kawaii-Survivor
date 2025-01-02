using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : DropableCurrency
{

    [Header("Elements")]
    public static Action<Cash> onCollected;
    protected override void Collected()
    {
        onCollected?.Invoke(this);
    }

}
