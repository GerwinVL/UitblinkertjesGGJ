using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jext;

public class GearManager : MonoBehaviour {

    [SerializeField]
    private KeyCode buttonSelectGear;
    private int chosenGear = 0;

    private void Start()
    {
        GearEditor.InitGears();
    }

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
            print(1);
            if (Physics.Raycast(ray, out hit))
            {
                gear = hit.transform.GetComponent<Gear>();
                if (gear != null)
                    chosenGear = gear.name.Last();
                print(chosenGear);
            }
        }
    }
}