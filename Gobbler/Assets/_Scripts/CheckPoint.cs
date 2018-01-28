using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public Transform checkPoint;

    public void Respawn()
    {
        transform.position = checkPoint.position;
    }
}
