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
    private List<Gear> gears;

    private void Start()
    {
        GearEditor.InitGears();
        gears = GearEditor.self.gears;

        gears.ForEach(gear => gear.child = null);
        foreach (Gear gear in gears)
            if (gear.parent != null)
                gear.parent.child = gear;

        SetupGearRotation();
    }

    [SerializeField]
    private string axisRotation = "Horizontal";
    private float axisValue;
    private Vector3 speed;
    private void Update()
    {
        // "The thing people don't realize about the Gear Wars ..."
        CheckIfNewGearSelected();

        axisValue = Input.GetAxis(axisRotation);
        if (Mathf.Approximately(0, axisValue))
            return;

        speed = chosenGear.transform.forward * axisValue * Time.deltaTime * gearSpeed;
        foreach (Gear gear in gears)
            gear.transform.Rotate(speed * (gear.right ? 1 : -1));
    }

    private Ray ray;
    private RaycastHit hit;
    private ChangeGear cGear;
    private bool right;
    private void CheckIfNewGearSelected()
    {
        if (Input.GetKeyDown(buttonSelectGear))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                cGear = hit.transform.GetComponent<ChangeGear>();
                if (cGear != null)
                {
                    if (chosenGear == cGear)
                        return;

                    chosenGear.SetColor(Color.white);
                    chosenGear = cGear;
                    SetupGearRotation();
                }
            }
        }
    }

    private Gear gear;
    private void SetupGearRotation()
    {
        chosenGear.SetColor(Color.red);

        gear = chosenGear.gear;
        gear.right = true;
        while(gear.parent != null)
        {
            gear.parent.right = !gear.right;
            gear = gear.parent;
        }

        while(gear.child != null)
        {
            gear.child.right = !gear.right;
            gear = gear.child;
        }
    }
}