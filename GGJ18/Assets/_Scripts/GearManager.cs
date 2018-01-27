using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jext;

public class GearManager : MonoBehaviour {

    [SerializeField]
    private KeyCode buttonSelectGear;
    private Gear chosenGear;

    private void Start()
    {
        GearEditor.InitGears();
        chosenGear = GearEditor.self.gears[0];
    }

    [SerializeField]
    private string axisRotation = "Horizontal";
    private float axisValue;
    private Ray ray;
    private RaycastHit hit;
    private Gear gear;
        
    private void Update()
    {
        // "The thing people don't realize about the Gear Wars ..."
        if (Input.GetKeyDown(buttonSelectGear))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                gear = hit.transform.GetComponent<Gear>();
                if (gear != null)
                    chosenGear = gear;
            }
        }

        axisValue = Input.GetAxis(axisRotation);


    }
}