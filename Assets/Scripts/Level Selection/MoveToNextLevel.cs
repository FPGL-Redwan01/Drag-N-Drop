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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)

    {
        if (other.collider.CompareTag("Player"))
        {
            StartCoroutine(MoveToNext());
        }
    }
    IEnumerator MoveToNext()
    {
        yield return new WaitForSeconds(3f);
        if (SceneManager.GetActiveScene().buildIndex == 9)
        {
           SceneManager.LoadScene(0);
        }
        else
        {

            SceneManager.LoadScene(nextSceneLoad);


            if (nextSceneLoad + 2 > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }
    }

}
