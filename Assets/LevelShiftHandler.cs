using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LevelShiftHandler : MonoBehaviour
{
    // Start is called before the first frame update
 //   [HideInInspector]
    public bool stageFlipped,startGame ;

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
        stageFlipped = true;
        this.transform.DORotate(new Vector3(0, 0, 180), 1f);
        

    }

    public void Play()
    {
        startGame = true;
        PlayerController.Instance._rigidbody.gravityScale = 1;
        PlayerController.Instance.moveSpeed = 6;
        
        
       
    }
    
}
