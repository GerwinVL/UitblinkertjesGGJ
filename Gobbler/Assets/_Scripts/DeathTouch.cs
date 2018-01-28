using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTouch : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.transform.tag == "Player")
        {
            c.gameObject.GetComponent<CheckPoint>().Respawn();
        }
    }
}
