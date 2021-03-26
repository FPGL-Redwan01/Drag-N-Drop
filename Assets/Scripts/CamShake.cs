using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public static CamShake Instance { get; private set; }

    Vector3 cameraInitialPos;
    public float shakeMagnitude = 0.05f, shakeTime = .5f;
    Camera mainCam;
    private void Awake()
    {
        Instance = this;
        mainCam = Camera.main;
    }
 public void ShakeIt ()
    {
        cameraInitialPos = mainCam.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", shakeTime);
    }

    void StartShake()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        Vector3 cameraIntermediatePos = mainCam.transform.position;
        cameraIntermediatePos.x += cameraShakingOffsetX;
        cameraIntermediatePos.y += cameraShakingOffsetY;
        mainCam.transform.position = cameraIntermediatePos;


    }

    void StopShake()
    {
        CancelInvoke("StartShake");
        mainCam.transform.position = cameraInitialPos;
    }
}
