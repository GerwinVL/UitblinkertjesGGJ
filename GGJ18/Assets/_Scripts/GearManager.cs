using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jext;

public class GearManager : MonoBehaviour {

    [SerializeField]
    private KeyCode buttonSelectGear;
    [SerializeField]
    private float gearSpeed;
    private Gear chosenGear;
    private List<Gear> gears;

    private void Start()
    {
        GearEditor.InitGears();
        gears = GearEditor.self.gears;
        chosenGear = GearEditor.self.gears[0];
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
    private Gear gear;
    private bool right;
    private void CheckIfNewGearSelected()
    {
        if (Input.GetKeyDown(buttonSelectGear))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                gear = hit.transform.GetComponent<Gear>();
                if (gear != null)
                {
                    if (chosenGear == gear)
                        return;

                    chosenGear.SetColor(Color.white);
                    chosenGear = gear;
                    SetupGearRotation();
                }
            }
        }
    }

    private void SetupGearRotation()
    {
        chosenGear.SetColor(Color.red);

        for (int gear = 0; gear < gears.Count; gear++)
            if (chosenGear == gears[gear])
                right = !Methods.IsEven(gear);

        gears[0].right = right;
        for (int gear = 1; gear < gears.Count; gear++)
        {
            gears[gear].right = !gears[gear].right;
        }
    }
}