using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private float upForce;
    [SerializeField] private Transform doorTransform;
    [SerializeField] private float checkDistance;
    private Rigidbody2D _rigidbody;


    [Header("FX")]
    public GameObject confetti;
    public GameObject popEffect;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveBalloon();
        AddForceWhileAtTheTop();
    }

    private void MoveBalloon()
    {
        if (_rigidbody.velocity.y < 3f)
        {
            _rigidbody.AddForce(Vector2.up * upForce, ForceMode2D.Force);    
        }
        else if (_rigidbody.velocity.y > 5f)
        {
            _rigidbody.AddForce(Vector2.down * upForce, ForceMode2D.Force);
        }
    }
    
    private void AddForceWhileAtTheTop()
    {
        RaycastHit2D[] raycastHitArray = Physics2D.RaycastAll(transform.position, Vector2.up, checkDistance);
        foreach (RaycastHit2D raycastHit in raycastHitArray)
        {
            if (raycastHit.collider.CompareTag("Border"))
            {
                Vector2 direction = doorTransform.position - transform.position;
                if (Mathf.Abs(_rigidbody.velocity.x) < 1f)
                {
                    _rigidbody.AddForce(direction * upForce * Time.deltaTime, ForceMode2D.Force);    
                }
                else if (Mathf.Abs(_rigidbody.velocity.x) > 2f)
                {
                    _rigidbody.AddForce(-direction * upForce * Time.deltaTime, ForceMode2D.Force);
                }
                // Debug.Log("Adding Force");
            }
        }
    }
    public void AddForceToDirection(Vector2 direction, float forceAmount)
    {
        _rigidbody.AddForce(direction * forceAmount * Time.deltaTime, ForceMode2D.Impulse);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Spike"))
        {
            UIManager.Instance.ShowCompletionMessage();
            SoundManager.sharedInstance.PlaySFX(SoundManager.sharedInstance.popSFX);           
            GameObject g = Instantiate(popEffect, this.transform.position, Quaternion.identity);
            Destroy(g, 2);
            Destroy(this.gameObject);
            FindObjectOfType<DragFan>().sfxAuidoSource.Pause();
        }
        if (other.collider.CompareTag("Door"))
        {
            UIManager.Instance.ShowCompletionMessage();
            
            GameObject fireWork = Instantiate(confetti , doorTransform.position , Quaternion.identity);
            Destroy(fireWork, 5);
            FindObjectOfType<DragFan>().sfxAuidoSource.Pause();
            SoundManager.sharedInstance.PlaySFX(SoundManager.sharedInstance.levelComSFX);
            Destroy(this.gameObject);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.up * checkDistance);
    }
}
