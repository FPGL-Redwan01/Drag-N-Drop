using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateFan : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Update()
    {
        Debug.Log(transform.eulerAngles.z);
    }
}
