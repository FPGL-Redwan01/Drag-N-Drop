using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Balloon : MonoBehaviour
{
    [SerializeField] private float upForce;
    [SerializeField] private float checkDistance;
    [SerializeField] private Transform doorTransform;
    private Rigidbody2D _rigidbody;


    [Header("FX")] public GameObject confetti;
    public GameObject popEffect;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveBalloon();
        Debug.Log(CanMoveToTarget(doorTransform));
        if (CanMoveToTarget(doorTransform))
        {
            AddForceWhileAtTheTop(doorTransform);
        }
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

    private bool CanMoveToTarget(Transform targetTransform)
    {
        RaycastHit2D[] raycastHitArray = Physics2D.LinecastAll(transform.position, targetTransform.position);
        foreach (RaycastHit2D raycastHit in raycastHitArray)
        {
            if (raycastHit.collider.CompareTag("Border"))
            {
                return false;
            }
        }

        return true;
    }

    private void AddForceWhileAtTheTop(Transform targetTransform)
    {
        RaycastHit2D[] raycastHitArray = Physics2D.RaycastAll(transform.position, Vector2.up, checkDistance);
        foreach (RaycastHit2D raycastHit in raycastHitArray)
        {
            if (raycastHit.collider.CompareTag("Border"))
            {
                Vector2 direction = targetTransform.position - transform.position;
                if (Mathf.Abs(_rigidbody.velocity.x) < .5f)
                {
                    _rigidbody.AddForce(direction * upForce * Time.deltaTime, ForceMode2D.Force);
                }
                else if (Mathf.Abs(_rigidbody.velocity.x) > 1f)
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
            LevelFailedEffect();
        }

        if (other.collider.CompareTag("Door"))
        {
            LevelCompleteEffect();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            LevelFailedEffect();
        }
    }

    private void LevelCompleteEffect()
    {
        UIManager.Instance.ShowCompletionMessage();
        SoundManager.sharedInstance.PlaySFX(SoundManager.sharedInstance.levelComSFX);
        GameObject fireWork = Instantiate(confetti, doorTransform.position, Quaternion.identity);
        Destroy(fireWork, 5);
        DestroyBalloon();
    }

    private void LevelFailedEffect()
    {
        UIManager.Instance.Invoke("ShowCompletionMessage", 1f);
        SoundManager.sharedInstance.PlaySFX(SoundManager.sharedInstance.popSFX);
        GameObject g = Instantiate(popEffect, this.transform.position, Quaternion.identity);
        Destroy(g, 2);
        DestroyBalloon();
        StartCoroutine(restartCurrentScene());
    }
    
    private void DestroyBalloon()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.GetComponent<CircleCollider2D>().enabled = false;
        FindObjectOfType<DragFan>().sfxAuidoSource.Pause();
    }

    IEnumerator restartCurrentScene()
    {
        yield return new WaitForSeconds(3.5f);

        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.up * checkDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, doorTransform.position);
        Gizmos.color = Color.green;
    }
}
