using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fan : MonoBehaviour
{
    [SerializeField] private float forceRadius;
    public float forceAngle;
    public DragFan df;


    private void Start()
    {
        df = GetComponent<DragFan>();
    }
    private void Update()
    {
        if (df._isPlacedCorrectly)
            PushBalloon();

    }
    
    private void PushBalloon()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, forceRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D.CompareTag("Player"))
            {
                Balloon balloon = collider2D.GetComponent<Balloon>();
                Vector2 forceDirection = balloon.transform.position - transform.position;
                Debug.DrawRay(transform.position, forceDirection);
                float angle = Vector2.Angle(forceDirection, transform.up);
                if (angle <= forceAngle)
                {
                    balloon.AddForceToDirection(forceDirection,50f);
                    // Debug.Log("Force Applied");
                }
                else
                {
                    // Debug.Log("No Force Applied");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, forceRadius);
    }
}
