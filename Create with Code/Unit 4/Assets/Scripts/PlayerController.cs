using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hasPowerup = false;
    public float speed = 5.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.transform.position = transform.position -
            new Vector3(0, 0.6f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {       
        if (!hasPowerup && other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(Countdown());
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidingObject = collision.gameObject;
        if (hasPowerup && collidingObject.CompareTag("Enemy"))
        {
            Vector3 p = transform.position;
            Vector3 e = collidingObject.transform.position;
            Vector3 awayFromPlayer = (e - p).normalized;
            Vector3 specialPush = awayFromPlayer * powerupStrength;
            Rigidbody enemyRb = collidingObject.GetComponent<Rigidbody>();
            enemyRb.AddForce(specialPush, ForceMode.Impulse);
            Debug.Log("Collided with " + gameObject.name + 
                ", knocked it back with an impulse of " + specialPush);
        }
    }
}
