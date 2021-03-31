using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFan : MonoBehaviour
{

    public GameObject normalFan, draggedFan;
    public Transform balloon;
    public GameObject blowParticles;
    [SerializeField] private float checkRadius;
    [SerializeField] private float offset;
    private Vector3 _mouseOffset;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Fan _fan;
    [HideInInspector]
    public bool isPlacedCorrectly;
    public AudioSource sfxAuidoSource;

    private void Awake()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _fan = GetComponent<Fan>();
    }
    
    private void Update()
    {
        if (isPlacedCorrectly)
        {
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
            normalFan.transform.GetChild(2).Rotate(0, 0, 1 * 2);
            blowParticles.SetActive(true);
          //  Vector3 direction = UtilsClass.GetMouseWorldPosition() - transform.position;
            // Debug.DrawRay(transform.position, direction);
          //  transform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(direction) + offset);
        }
    }

    private void OnMouseDown()
    {
        _mouseOffset = transform.position - UtilsClass.GetMouseWorldPosition();
        isPlacedCorrectly = false;
        sfxAuidoSource.Pause();
    }

    private void OnMouseDrag()
    {
        if (balloon.position != null)
        {
            blowParticles.SetActive(false);
            transform.position = UtilsClass.GetMouseWorldPosition() + _mouseOffset;
            Vector3 direction = balloon.position - transform.position;

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
                isPlacedCorrectly = false;
                break;
            }
            if (collider2D.CompareTag("Border"))
            {
                SoundManager.sharedInstance.PlaySFX(SoundManager.sharedInstance.fanSetSFX);
                sfxAuidoSource.Play();
                isPlacedCorrectly = true;
            }
        }

        if (!isPlacedCorrectly)
        {
            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
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
