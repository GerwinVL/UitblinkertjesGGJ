using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAsset : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private bool right;

    private Vector3 rot;
	private void Update () {
        transform.Rotate(speed * Time.deltaTime * transform.forward * (right ? -1 : 1));
        rot = transform.eulerAngles;
        rot.x = 0;
        transform.eulerAngles = rot;
    }
}
