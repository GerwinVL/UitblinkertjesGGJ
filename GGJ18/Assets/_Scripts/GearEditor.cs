using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[ExecuteInEditMode]
public class GearEditor : MonoBehaviour {

    private float dis, radians, x, y;
    private Vector3 root, ret;
    private Gear[] gearsArr;
    private List<Gear> gears;

    public void ConnectAllGears()
    {
        gearsArr = FindObjectsOfType(typeof(Gear)) as Gear[];
        foreach (Gear gear in gearsArr)
            gears.Add(gear);
        gears.Sort();

        foreach (Gear gear in gears)
            ConnectGears(gear);
    }

    public void ConnectGears(Gear gear)
    {
        if (gear.parent == null)
            return;
        dis = gear.size / 2 + gear.parent.size / 2;
        root = gear.parent.transform.position;

        radians = gear.angle * Mathf.Deg2Rad;
        x = Mathf.Cos(radians);
        y = Mathf.Sin(radians);

        ret = new Vector3(x, y, 0) * dis + root;
        ret.z = root.z;
        gear.transform.position = ret;
    }
}

[CustomEditor(typeof(GearEditor))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GearEditor myScript = (GearEditor)target;
        if (GUILayout.Button("Connect"))
            myScript.ConnectAllGears();
    }
}