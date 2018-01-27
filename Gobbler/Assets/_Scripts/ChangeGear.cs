using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGear : MonoBehaviour {

    public Gear gear;
    private Renderer gearOutline;

    private void Awake()
    {
        gearOutline = GetComponent<Renderer>();
    }

    public void SetColor(Color color)
    {
        gearOutline.material.color = color;
    }
}
