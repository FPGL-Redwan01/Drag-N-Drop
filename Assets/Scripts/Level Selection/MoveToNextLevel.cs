using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    public int nextSceneLoad;

    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnCollisionEnter2D(Collision2D other)

    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(MoveToNext());
        }
    }
    IEnumerator MoveToNext()
    {
        yield return new WaitForSeconds(2f);
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene(1);


        }
        else
        {

            SceneManager.LoadScene(nextSceneLoad);


            if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }
    }

}
