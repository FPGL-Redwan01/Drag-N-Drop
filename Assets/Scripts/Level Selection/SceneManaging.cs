using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour
{

    public void ChangeScene(int sceneIndex)
    {

            SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeSceneForStart(int sceneIndex)
    {
        int a;
        a = PlayerPrefs.GetInt("TutorialPlayed");
        if (a == 1)
        {
            SceneManager.LoadScene(3);
        }
        else
        SceneManager.LoadScene(sceneIndex);

    }
    public void ChangeSceneForTutorial(int sceneIndex)
    {
        if (PlayerPrefs.GetInt("TutorialPlayed", 0) <= 0)
        {

            PlayerPrefs.SetInt("TutorialPlayed", 1);
        }
            SceneManager.LoadScene(sceneIndex);

    }

}
