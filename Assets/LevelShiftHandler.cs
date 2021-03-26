using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class LevelShiftHandler : MonoBehaviour
{
    // Start is called before the first frame update
 //   [HideInInspector]
    public bool stageFlipped,startGame ;
    public GameObject spring;
    public GameObject drops;

    public static LevelShiftHandler Instance { get; private set; }
    void Start()
    {
        
    }
    private void Awake()
    {
        Instance = this;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void RotateStage()
    {
        //stageFlipped = true;
        // this.transform.DORotate(new Vector3(0, 0, 180), 1f ).SetEase(Ease.InOutBack);
        SceneManager.LoadScene(0);

    }

    public void Play()
    {
       
        if(FindObjectOfType<DragObject>().placed)
        {
            drops.gameObject.SetActive(true);
        }
        else
            drops.gameObject.SetActive(false);
        spring.GetComponent<Animator>().Play("Spring");
        startGame = true;
        PlayerController.Instance._rigidbody.gravityScale = 1;
        PlayerController.Instance.moveSpeed = 6;
        
        
       
    }
    
}
