using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFan : MonoBehaviour
{

    public GameObject normalFan, draggedFan;
    public Transform baloon;
    public GameObject blowParticles;
    [SerializeField] private float checkRadius;
    [SerializeField] private float offset;
    private Vector3 _mouseOffset;
    private Vector3 _initialPosition;  
    private Transform _windFlowIndicatorPos;
    private Transform _windFlowIndicatorNeg;
    private Fan _fan;
    [HideInInspector]
    public bool _isPlacedCorrectly;

    public AudioSource sfxAuidoSource;

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
       
           normalFan.transform.GetChild(2).Rotate(0, 0, 1 * 2);
            blowParticles.SetActive(true);
            _windFlowIndicatorPos.gameObject.SetActive(true);
            _windFlowIndicatorNeg.gameObject.SetActive(true);
            _windFlowIndicatorPos.transform.localEulerAngles = new Vector3(0, 0, _fan.forceAngle);
           _windFlowIndicatorNeg.transform.localEulerAngles = new Vector3(0, 0, -1 * _fan.forceAngle);
          //  Vector3 direction = UtilsClass.GetMouseWorldPosition() - transform.position;
            // Debug.DrawRay(transform.position, direction);
          //  transform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(direction) + offset);

            if (Input.GetMouseButtonDown(0))
            {
              //  Destroy(this);
            }    
        }
    }

    private void OnMouseDown()
    {
        _mouseOffset = transform.position - UtilsClass.GetMouseWorldPosition();
        _isPlacedCorrectly = false;
        sfxAuidoSource.Pause();
    }

    private void OnMouseDrag()
    {

        blowParticles.SetActive(false);
        transform.position = UtilsClass.GetMouseWorldPosition() + _mouseOffset;
        if (baloon.position != null)
        {
            Vector3 direction = baloon.position - transform.position;

            // Debug.DrawRay(transform.position, direction);
            transform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(direction) + offset);
        }
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
                SoundManager.sharedInstance.PlaySFX(SoundManager.sharedInstance.fanSetSFX);
                sfxAuidoSource.Play();
                _isPlacedCorrectly = true;
            }
        }

        if (!_isPlacedCorrectly)
        {
            transform.position = _initialPosition;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            normalFan.SetActive(true);
            draggedFan.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            normalFan.SetActive(false);
            draggedFan.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.color = Color.red;
    }
}
