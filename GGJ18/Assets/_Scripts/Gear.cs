using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Jext;

public class Gear : MonoBehaviour, IComparable<Gear> {

    public Gear parent;
    [Range(0, 359)]
    public int angle;
    public float size;

    public int CompareTo(Gear other)
    {
        return name.Last() - other.name.Last();
    }
}
