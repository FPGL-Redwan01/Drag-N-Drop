using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fan : MonoBehaviour
{
    [SerializeField] private float forceRadius;
    [SerializeField] private float forceAngle;
    [SerializeField] private Slider slider;

    private void Awake()
    {
    }

    private void Update()
    {
        RotateFan();
        PushBalloon();
    }

    private void RotateFan()
    {
        float rotationAngle = -1 * slider.value * forceAngle;
        transform.localEulerAngles = new Vector3(0,0,rotationAngle);
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
                float angle = Vector2.Angle(forceDirection, transform.right);
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
