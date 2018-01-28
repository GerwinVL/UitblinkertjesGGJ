using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialButtons : MonoBehaviour {

    public int specialID;
    private bool activated;
    public Animator button;
    public ChangeGear gear;

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
                        GearManager.self.ChangeToNewGear(gear);

                        break;
                }
            }
        }
    }
}
