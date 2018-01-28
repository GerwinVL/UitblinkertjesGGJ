using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    private bool activated;
    public int id;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.transform.tag == "Player")
        {
            if (!activated)
            {
                activated = true;
                switch (id)
                {
                    case 0:
                        print(GameManager.instance);
                        GameManager.instance.StartTimer();
                        break;
                    case 1:
                        print("done");
                        StartCoroutine(GameManager.instance.ShowScores(GameManager.instance.EndTimer()));
                        break;
                }
            }
        }
    }
}
