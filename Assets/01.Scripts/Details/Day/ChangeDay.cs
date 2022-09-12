using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDay : MonoBehaviour
{
    public GameObject sun;
    public GameObject moon;

    public float dayTime;
    public float night;


    void Update()
    {
        SunMove();

        ActiveSun();
    }

    private void ActiveSun()
    {
        if (Mathf.Abs(transform.eulerAngles.z) > 90f && Mathf.Abs(transform.eulerAngles.z) < 91f) { sun.SetActive(true); moon.SetActive(false); }
        else if(Mathf.Abs(transform.eulerAngles.z) > 250f && Mathf.Abs(transform.eulerAngles.z) < 255f) { sun.SetActive(false); moon.SetActive(true); }
    }

    private void SunMove()
    {
        //float aa = Mathf.Abs(transform.rotation.z);

        if (Mathf.Abs(transform.eulerAngles.z) > 90f)
            //transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.Euler(new Vector3(0, 0, dayTime)), 0.3f);
            transform.Rotate(new Vector3(0, 0, dayTime) * Time.deltaTime);
        else
            //transform.rotation = Quaternion.Lerp(transform.rotation, transform.Rotate(new Vector3(0, 0, night), Space.Self), 0.3f);
            transform.Rotate(new Vector3(0, 0, night) * Time.deltaTime);
    }
}
