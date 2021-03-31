using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;

    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 4); 
        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 4 > levelAt)
            {
                lvlButtons[i].transform.Find("disableImage").gameObject.SetActive(true);
                lvlButtons[i].transform.Find("enableImage").gameObject.SetActive(false);
                lvlButtons[i].interactable = false;
            }
            else
            {
                lvlButtons[i].transform.Find("disableImage").gameObject.SetActive(false);
                lvlButtons[i].transform.Find("enableImage").gameObject.SetActive(true);
                lvlButtons[i].interactable = true;
            }
        }
    }

}
