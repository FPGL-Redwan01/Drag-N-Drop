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

    private void Awake()
    {
        Instance = this;
     //   _message = levelCompletionMessage.transform.Find("text").GetComponent<TextMeshProUGUI>();
        _image = levelCompletionMessage.transform.Find("image").GetComponent<Image>();
        levelCompletionMessage.SetActive(false);
    }

    public void ShowCompletionMessage(string message)
    {
      //  _message.text = message;
        levelCompletionMessage.SetActive(true);
        _image.gameObject.SetActive(false);
    }
    public void ShowCompletionMessage()
    {
       // _message.text = "";
        levelCompletionMessage.SetActive(true);
      //  _message.gameObject.SetActive(false);
    }
}
