using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;
using UnityEngine.VFX;

public class ChangeDay : MonoBehaviour
{
    public GameObject sun;
    public GameObject moon;

    public float dayTime;
    public float night;
    public float currentSpeed;

    public Volume _volume;
    public HDRISky _hdrisky;

    public HDAdditionalLightData _light;

    private void Start()
    {
        var profiles = _volume.sharedProfile;
        profiles.TryGet<HDRISky>(out _hdrisky);

        _hdrisky.exposure.value = 5f;
        _light.intensity = 0;

        StartCoroutine(ChangeSky());
    }

    void Update()
    {
        SunMove();

        ActiveSun();
    }

    private void ActiveSun()
    {
        if (Mathf.Abs(transform.eulerAngles.z) > 90f && Mathf.Abs(transform.eulerAngles.z) < 91f) { sun.SetActive(true); moon.SetActive(false); }
        else if (Mathf.Abs(transform.eulerAngles.z) > 250f && Mathf.Abs(transform.eulerAngles.z) < 255f) { sun.SetActive(false); moon.SetActive(true); }
    }

    private void SunMove()
    {
        //float aa = Mathf.Abs(transform.rotation.z);

        //Debug.Log("1:" + transform.localRotation.z);
        //Debug.Log("2:" + transform.eulerAngles.z);
        //Debug.Log("3:" + transform.rotation.z);

        if (transform.eulerAngles.z < 90f || transform.eulerAngles.z > 270f)
        {
            currentSpeed = night;
            if (_hdrisky.exposure.value >= 5)
            {
                _hdrisky.exposure.value -= 0.1f;
            }

            if (_light.intensity >= 0)
            {
                _light.intensity -= 3f;
            }
        }

        else //if(Mathf.Abs(transform.eulerAngles.z) < 270f && Mathf.Abs(transform.eulerAngles.z) < 90f)
        {
            currentSpeed = dayTime;
            if (_hdrisky.exposure.value <= 15)
            {
                _hdrisky.exposure.value += 0.1f;
            }
            if (_light.intensity <= 600)
            {
                _light.intensity += 3f;
            }
        }

        transform.rotation *= Quaternion.Euler(0, 0, 0.1f);
        //transform.Rotate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    IEnumerator ChangeSky()
    {
        while (true)
        {
            if (_hdrisky.rotation.value == _hdrisky.rotation.max)
                _hdrisky.rotation.value = 0;

            _hdrisky.rotation.value += 0.04f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
