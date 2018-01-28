using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesButton : MonoBehaviour {

    public Animator button;
    public Animator[] spikes;
    private bool activated;

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.transform.tag == "Player")
        {
            if (!activated)
            {
                activated = true;
                button.SetTrigger("Triggered");
                for (int i = 0; i < spikes.Length; i++)
                {
                    spikes[i].SetTrigger("Active");
                }
            }
        }
    }
}
