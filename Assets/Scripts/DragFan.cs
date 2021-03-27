using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFan : MonoBehaviour
{
    [SerializeField] private float checkRadius;
    [SerializeField] private float offset;
    private Vector3 _mouseOffset;
    private Vector3 _initialPosition;
    private bool _isPlacedCorrectly;
    private Transform _windFlowIndicatorPos;
    private Transform _windFlowIndicatorNeg;
    private Fan _fan;

    private void Awake()
    {
        _initialPosition = transform.position;
        _windFlowIndicatorPos = transform.Find("windflowPos");
        _windFlowIndicatorNeg = transform.Find("windflowNeg");
        
        _windFlowIndicatorPos.gameObject.SetActive(false);
        _windFlowIndicatorNeg.gameObject.SetActive(false);
    }

    private void Start()
    {
        _fan = GetComponent<Fan>();
    }

    private void Update()
    {
        if (_isPlacedCorrectly)
        {
            _windFlowIndicatorPos.gameObject.SetActive(true);
            _windFlowIndicatorNeg.gameObject.SetActive(true);
            _windFlowIndicatorPos.transform.localEulerAngles = new Vector3(0, 0, _fan.forceAngle);
            _windFlowIndicatorNeg.transform.localEulerAngles = new Vector3(0, 0, -1 * _fan.forceAngle);
            Vector3 direction = UtilsClass.GetMouseWorldPosition() - transform.position;
            // Debug.DrawRay(transform.position, direction);
            transform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(direction) + offset);

            if (Input.GetMouseButtonDown(0))
            {
                Destroy(this);
            }    
        }
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
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, checkRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D.CompareTag("Spike"))
            {
                _isPlacedCorrectly = false;
                break;
            }
            if (collider2D.CompareTag("Border"))
            {
                _isPlacedCorrectly = true;
            }
        }

        if (!_isPlacedCorrectly)
        {
            transform.position = _initialPosition;
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.color = Color.red;
    }
}
