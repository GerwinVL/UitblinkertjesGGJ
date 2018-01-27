using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhettoKey : MonoBehaviour
{

    public KeyCode key = KeyCode.Mouse0;

	void Update ()
    {
        if (Input.GetKey(key))
        {
            Debug.Log("Input: " + key);
        }
	}
}
