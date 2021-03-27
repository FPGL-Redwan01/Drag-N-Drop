using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFan : MonoBehaviour
{
    [SerializeField] private float checkRadius;
    private Vector3 _mouseOffset;
    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        _mouseOffset = transform.position - UtilsClass.GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = UtilsClass.GetMouseWorldPosition() + _mouseOffset;
    }

    private void OnMouseUp()
    {
        bool isCorrectlyPlaced = false;
        
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, checkRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D.CompareTag("Spike"))
            {
                isCorrectlyPlaced = false;
                break;
            }
            if (collider2D.CompareTag("Border"))
            {
                isCorrectlyPlaced = true;
            }
        }

        if (!isCorrectlyPlaced)
        {
            transform.position = _initialPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.color = Color.red;
    }
}
