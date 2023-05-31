using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int pointValue;
    public bool canMiss;
    private float minSpeed = 15f;
    private float maxSpeed = 18f;
    private float xRange = 4f;
    private float yPosition = -6f;
    private float maxTorque = 10f;

    public ParticleSystem explosion;
    private Rigidbody targetRb;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = RandomPosition();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), ForceMode.Impulse);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private Vector3 RandomPosition() {
        return new Vector3(
            Random.Range(-xRange, xRange), // random x
            yPosition, // fixed y
            0);  // fixed z
    }

    private Vector3 RandomForce() {
        return Vector3.up * Random.Range(minSpeed, maxSpeed); // upward force
    }

    private Vector3 RandomTorque() {
        return new Vector3(
            Random.Range(-maxTorque, maxTorque), // x torque
            Random.Range(-maxTorque, maxTorque), // y torque
            Random.Range(-maxTorque, maxTorque)); // z torque
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        Destroy(gameObject);
        gameManager.UpdateScore(pointValue);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canMiss)
        {
            gameManager.GameOver();
        }
        Destroy(gameObject);
    }
}
