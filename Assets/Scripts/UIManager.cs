using System;
using System.Collections;
using System.Collections.Generic;
//using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject levelEnd , endTran;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowCompletionMessage()
    {
        levelEnd.SetActive(true);
        Invoke("EndTransition", 1.5f);
    }

    public void EndTransition()
    {
        endTran.gameObject.SetActive(true);
    }
}
