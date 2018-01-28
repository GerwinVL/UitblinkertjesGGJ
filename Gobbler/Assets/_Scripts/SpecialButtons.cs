using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class SpecialButtons : MonoBehaviour {

    public int specialID;
    private bool activated;
    public Animator button;
    public ChangeGear gear;
    public Vector3 camPos;
    private float camSpeed = 10;
    public Animator door;
    public PostProcessingProfile pProfile;

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
                        StartCoroutine(LerpPostProcessing());
                        break;
                    case 1:
                        StartCoroutine(MoveCamera());
                        StartCoroutine(LerpPostProcessingBack());
                        break;
                }
            }
        }
    }

    private IEnumerator LerpPostProcessingBack()
    {
        float speed = 5, val = 0, progression = 0;

        while(val < 147)
        {
            yield return null;
            progression += speed * Time.deltaTime;
            val = Mathf.Lerp(47, 147, progression);
            DepthOfFieldModel.Settings dofm = pProfile.depthOfField.settings;
            dofm.focalLength = val;
            pProfile.depthOfField.settings = dofm;
        }
    }

    private IEnumerator LerpPostProcessing()
    {
        float speed = 5, val = 0, progression = 0;

        while (val > 47)
        {
            yield return null;
            progression += speed * Time.deltaTime;
            val = Mathf.Lerp(147, 47, progression);
            DepthOfFieldModel.Settings dofm = pProfile.depthOfField.settings;
            dofm.focalLength = val;
            pProfile.depthOfField.settings = dofm;
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
