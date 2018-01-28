using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAsset : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private bool right;
	
	private void Update () {
        transform.Rotate(speed * Time.deltaTime * transform.forward * (right ? -1 : 1));
    }
}
