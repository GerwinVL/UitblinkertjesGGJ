using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public gameObject mainPage;
    public gameObject creditsPage;

    public void ButtonInput(int i)
    {
        switch (i)
        {
            //Credits page
            case 0:
                mainPage.SetActive(false);
                creditsPage.SetActive(false);
                break;
        }
    }
}
