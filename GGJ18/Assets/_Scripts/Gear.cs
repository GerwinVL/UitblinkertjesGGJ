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
    [HideInInspector]
    public bool right;
    [SerializeField]
    private Renderer gearOutline;

    public void SetColor(Color color)
    {
        gearOutline.material.color = color;
    }

    public int CompareTo(Gear other)
    {
        return name.Last() - other.name.Last();
    }
}
