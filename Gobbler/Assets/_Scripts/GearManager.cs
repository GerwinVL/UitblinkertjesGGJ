using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jext;

public class GearManager : MonoBehaviour
{
    [SerializeField]
    private KeyCode buttonSelectGear;
    [SerializeField]
    private float gearSpeed;
    [SerializeField]
    private ChangeGear chosenGear;
    private List<Gear> gears, childs;
    private Gear child;
    [SerializeField]
    private bool inverseRotation;

    private void Start()
    {
        GearEditor.InitGears();
        gears = GearEditor.self.gears;

        gears.ForEach(gear => gear.childs = new List<Gear>());
        foreach (Gear gear in gears)
            if (gear.parent != null)
                gear.parent.childs.Add(gear);
        
        chosenGear.gear.right = inverseRotation;
        childs = new List<Gear>();

        foreach (Gear gear in chosenGear.gear.childs)
        {
            gear.right = !chosenGear.gear.right;
            childs.Add(gear);
        }

        while(childs.Count > 0)
        {
            child = childs[0];
            childs.RemoveAt(0);

            foreach (Gear gear in child.childs)
            {
                gear.right = !chosenGear.gear.right;
                childs.Add(gear);
            }
        }

        SetupGearRotation();
    }

    [SerializeField]
    private string axisRotation = "Horizontal";
    private float axisValue;
    private Vector3 speed;
    private void Update()
    {
        // "The thing people don't realize about the Gear Wars ..."
        axisValue = Input.GetAxis(axisRotation);
        if (Mathf.Approximately(0, axisValue))
            return;

        speed = chosenGear.transform.forward * axisValue * Time.deltaTime * gearSpeed;
        foreach (Gear gear in gears)
            gear.transform.Rotate(speed * (gear.right ? 1 : -1));
    }

    public void ChangeToNewGear(ChangeGear cGear)
    {
        chosenGear.SetColor(Color.white);
        chosenGear = cGear;
        SetupGearRotation();
    }

    private void SetupGearRotation()
    {
        chosenGear.SetColor(Color.red);

        if(!chosenGear.gear.right)
            foreach (Gear gear in gears)
                gear.right = !gear.right;
    }
}