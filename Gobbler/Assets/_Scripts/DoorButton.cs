using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour {

    private bool activated;
    //public GameObject door;
    public Animator door;
    public Animator button;
    private float targetPosZ;
    private float moveSpeed = 3f;

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.transform.tag == "Player")
        {
            if (!activated)
            {
                activated = true;
                button.SetTrigger("Triggered");
                door.SetTrigger("Open");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.transform.tag == "Player")
        {
            if (!activated)
            {
                activated = true;
                button.SetTrigger("Triggered");
                door.SetTrigger("Open");
            }
        }
    }
}
