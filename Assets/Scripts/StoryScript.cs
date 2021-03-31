using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StoryScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ballon , ui ;
    public GameObject pp, me, drone;
    public Transform dronePos, myPos, ppPos;

    public GameObject myUi, ppUi;
    void Start()
    {
        StartCoroutine(StartStory());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartStory()
    {
        yield return new WaitForSeconds(4);
        ui.gameObject.SetActive(true);
        MovePP();
    }
    void MovePP()
    {
        SoundManager.sharedInstance.PlaySFX(SoundManager.sharedInstance.ashtonisedSFX);
        pp.transform.DOMove(ppPos.position , 2).OnComplete(() =>
        {
            MoveMySelf();
            Invoke("EnablePpUi", 0);
            Invoke("EnableMyUi", 2);
        });

    }
    void EnableMyUi()
    {
        myUi.gameObject.SetActive(true);
    }
    void EnablePpUi()
    {
        ppUi.gameObject.SetActive(true);
    }

    void MoveMySelf()
    {
    
        me.transform.DOMove(myPos.position, 3).OnComplete(() =>
        {
            SoundManager.sharedInstance.PlaySFX(SoundManager.sharedInstance.happySFX);
            MoveDrone();
        });
    }


    void MoveDrone()
    {
        drone.transform.DOMove(dronePos.position, 2.5f);
    }
}
