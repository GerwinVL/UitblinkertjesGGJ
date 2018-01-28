using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject mainPage;
    public GameObject creditsPage;

    public void ButtonInput(int i)
    {
        switch (i)
        {
            //Credits page
            case 0:
                mainPage.SetActive(false);
                creditsPage.SetActive(true);
                break;
			//Play Game
		case 1:
			
			break;
			//Exit Gamea
		case 2:

			break;
        }
    }
}
