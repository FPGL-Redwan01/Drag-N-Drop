using System;
using System.Collections;
using System.Collections.Generic;
//using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject levelCompletionMessage;
    //private TextMeshProUGUI _message;
    private Image _image;
    public GameObject levelEnd , endTran;

    private void Awake()
    {
        Instance = this;

        _image = levelCompletionMessage.transform.Find("image").GetComponent<Image>();
        levelCompletionMessage.SetActive(false);
    }

    public void ShowCompletionMessage(string message)
    {

        levelCompletionMessage.SetActive(true);
        _image.gameObject.SetActive(false);
    }
    public void ShowCompletionMessage()
    {
        levelEnd.SetActive(true);
        Invoke("EndTransition", 1.5f);
        levelCompletionMessage.SetActive(true);
  
    }

    public void EndTransition()
    {
        endTran.gameObject.SetActive(true);
    }
}
