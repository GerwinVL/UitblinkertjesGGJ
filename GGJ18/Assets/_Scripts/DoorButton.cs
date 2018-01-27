using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour {

	private bool activated;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            print("It works");
        }
    }
}
