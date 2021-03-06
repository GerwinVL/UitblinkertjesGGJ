﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour {

    public Transform ballPos;
    public Vector3 camPos;
    public ChangeGear gear;
    private float camSpeed = 10;
    private bool activated;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.transform.tag == "Player")
        {
            if (!activated)
            {
                activated = true;
                StartCoroutine(MoveCamera());
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
                GameObject ball = ObjectList.instance.ball;
                ball.transform.position = new Vector3(ballPos.position.x, ballPos.position.y, ball.transform.position.z);
                ball.GetComponent<CheckPoint>().checkPoint = ballPos;
                GearManager.self.ChangeToNewGear(gear);
            }
        }
        
    }
}
