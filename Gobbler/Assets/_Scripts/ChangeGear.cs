using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGear : MonoBehaviour {

    public Gear gear;
    private Renderer gearOutline;
    private Color baseColor;

    private void Awake()
    {
        gearOutline = GetComponent<Renderer>();
        baseColor = gearOutline.material.color;
    }

    public void SetColor(Color color)
    {
        gearOutline.material.color = color;
    }

    public void ResetColor()
    {
        gearOutline.material.color = baseColor;
    }
}
