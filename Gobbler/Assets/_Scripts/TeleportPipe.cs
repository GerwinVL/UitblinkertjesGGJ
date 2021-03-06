﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPipe : MonoBehaviour {

    public bool activated;
    public Transform ballPos;
    public TeleportPipe recievingEnd;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.transform.tag == "Player")
        {
            if (!activated)
            {
                activated = true;
                recievingEnd.activated = true;
                recievingEnd.CallCooldown();
                GameObject ball = c.transform.gameObject;
                ball.transform.position = new Vector3(ballPos.position.x, ballPos.position.y, ball.transform.position.z);
                StartCoroutine(Cooldown());
            }
        }
    }

    public void CallCooldown()
    {
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        activated = false;
    }
}
