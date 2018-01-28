#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[ExecuteInEditMode]
public class GearEditor : MonoBehaviour
{
    public static GearEditor self;

    public bool update;
    private float dis, radians, x, y;
    private Vector3 root, ret;
    private Gear[] gearsArr;
    [HideInInspector]
    public List<Gear> gears = new List<Gear>();

    private void Awake()
    {
        self = this;
    }

    private void Update()
    {
        if (update)
            ConnectAllGears();
    }

    public static void InitGears()
    {
        self.InitGearList();
    }

    public void InitGearList()
    {
        gearsArr = FindObjectsOfType(typeof(Gear)) as Gear[];
        gears.Clear();
        foreach (Gear gear in gearsArr)
            gears.Add(gear);
        gears.Sort();
    }

    public void ConnectAllGears()
    {
        InitGearList();
        foreach (Gear gear in gears)
            ConnectGears(gear);
    }

    private void ConnectGears(Gear gear)
    {
        if (gear.parent == null)
            return;
        
        gear.transform.position = GetConnectionPos(gear);
    }

    public Vector3 GetConnectionPos(Gear gear)
    {
        dis = gear.size / 2 + gear.parent.size / 2;
        root = gear.parent.transform.position;

        radians = gear.angle * Mathf.Deg2Rad;
        x = Mathf.Cos(radians);
        y = Mathf.Sin(radians);

        ret = new Vector3(x, y, 0) * dis + root;
        ret.z = root.z;
        return ret;
    }
}

[CustomEditor(typeof(GearEditor))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GearEditor myScript = (GearEditor)target;
        if (!myScript.update)
            if (GUILayout.Button("Connect"))
                myScript.ConnectAllGears();
    }
}
#endif