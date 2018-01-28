using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialButtons : MonoBehaviour {

    public int specialID;
    private bool activated;
    public Animator button;
    public ChangeGear gear;
    public Vector3 camPos;
    private float camSpeed = 10;
    public Animator door;

    private void OnCollisionEnter2D(Collision2D c)
    {
        if(c.transform.tag == "Player")
        {
            if (!activated)
            {
                activated = true;
                button.SetTrigger("Triggered");
                switch (specialID)
                {
                    case 0:
                        StartCoroutine(MoveCamera());
                        break;
                    case 1:
                        StartCoroutine(MoveCamera());
                        break;
                }
            }
        }
    }

    private IEnumerator MoveCamera()
    {
        bool moved = false;
        GameObject cam = ObjectList.instance.cam;
        print(cam);
        Vector3 targetPos = new Vector3(camPos.x, camPos.y, camPos.z);
        while (!moved)
        {
            yield return new WaitForEndOfFrame();
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, camPos, camSpeed * Time.deltaTime);
            if (cam.transform.position.ToString() == targetPos.ToString())
            {
                moved = true;
                GearManager.self.ChangeToNewGear(gear);
            }
        }
        if(specialID == 1)
        {
            door.SetTrigger("Open");
        }
    }
}
