using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 _mouseOffset;
    private Vector3 _initialPosition;
    private Transform _parentTransform;
    [SerializeField] private float checkRadius;
   public bool placed;
    private void Start()
    {
        _initialPosition = transform.position;
        _parentTransform = transform.parent;
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
        placed = true;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, checkRadius);
        
        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D.CompareTag("InstructionPoint"))
            {
                if (collider2D.transform.childCount == 0)
                {
                    transform.position = collider2D.transform.position;
                    transform.parent = collider2D.transform;
                    InstructionManager.Instance.GiveInstruction();
                    return;    
                }
            }
        }
       // RestorePosition();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RestorePosition();
            InstructionManager.Instance.GiveInstruction();
        }
    }

    public void RestorePosition()
    {
        transform.position = _initialPosition;
        transform.parent = _parentTransform;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
