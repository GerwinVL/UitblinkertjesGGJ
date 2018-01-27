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
                //   targetPosZ = door.transform.position.z + 1.5f;
                // StartCoroutine(MoveDoor());
                //door.SetActive(false);
                button.SetTrigger("Triggered");
                door.SetTrigger("Open");
            }
        }
    }

    private IEnumerator MoveDoor()
    {
        bool moved = false;
        while (!moved)
        {
            yield return new WaitForEndOfFrame();
            Vector3 targetPos = new Vector3(door.transform.position.x,door.transform.position.y, targetPosZ);
            door.transform.position = Vector3.MoveTowards(door.transform.position, targetPos, moveSpeed * Time.deltaTime);
            if (door.transform.position.z.ToString() == targetPosZ.ToString())
            {
                moved = true;
            }
        }
    }
}
