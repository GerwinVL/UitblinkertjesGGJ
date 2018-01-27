using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectList : MonoBehaviour {

	public static ObjectList instance;
    public GameObject ball;
    public GameObject cam;

    private void Awake()
    {
        instance = this;
    }
}
