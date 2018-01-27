using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Jext;

public class Gear : MonoBehaviour, IComparable<Gear>
{
    public Gear parent;
    [HideInInspector]
    public Gear child;
    [Range(0, 359)]
    public int angle;
    public float size;
    [HideInInspector]
    public bool right;

    public int CompareTo(Gear other)
    {
        return name.Last() - other.name.Last();
    }
}
