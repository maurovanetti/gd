using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    public GameObject powerupIndicator;
    public GameObject rocketPrefab;
    public float speed = 0.5f;
    public float powerupStrength = 30.0f;
    public float dashStrength = 60.0f;
    
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private bool hasShield;

    // Start is called before the first frame update
    void Start()
    {
        hasShield = false;
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.SetActive(hasShield);
        powerupIndicator.transform.position = transform.position +
            new Vector3(0, -0.5f, 0);


        if (Input.GetKeyDown(KeyCode.R))
        {
            ShootRockets();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            transform.position = focalPoint.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Defence Powerup"))
        {
            hasShield = true;
            Destroy(other.gameObject);

            StartCoroutine(PowerupCountdownRoutine());
        }
        if (other.CompareTag("Attack Powerup"))
        {
            Destroy(other.gameObject);

            ShootRockets();
        }
        if (other.CompareTag("Evasion Powerup"))
        {
            Destroy(other.gameObject);

            Recenter();
        }
    }

    private void Recenter()
    {
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
        transform.position = focalPoint.transform.position;
    }

    private void ShootRockets()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i];
            Instantiate(rocketPrefab,
                transform.position,
                Quaternion.LookRotation(
                    Vector3.up,
                    (enemy.transform.position - transform.position)
                    ));
        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasShield = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasShield)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position -
                transform.position;
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

            Debug.Log("Collided with " + collision.gameObject.name +
                " with hasShield=" + hasShield +
                " at speed " + playerRb.velocity.magnitude);
        }
    }
}
