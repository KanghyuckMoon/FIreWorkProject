using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChange : MonoBehaviour
{
    private GameObject sun;
    private GameObject moon;

    public float dayTime;
    public float night;

    public float time;

    void Start()
    {
        sun = transform.Find("SUN").GetComponent<GameObject>();
        moon = transform.Find("Moon").GetComponent<GameObject>();

        StartCoroutine(ChangeSunMoon());
    }

    IEnumerator ChangeSunMoon()
    {
        while (true)
        {
            if(transform.rotation.z % 360 > 180)
                transform.rotation *= Quaternion.Euler(0, 0, dayTime);
            else
                transform.rotation *= Quaternion.Euler(0, 0, night);

            yield return new WaitForSecondsRealtime(time);

            if (transform.rotation.z > 360)
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
