using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhettoKey : MonoBehaviour
{
    public KeyCode click = KeyCode.Mouse0;

	void Update ()
    {
        if (Input.GetKey(click))
        {
            Debug.Log("Clicked" + click);
        }
	}
}
