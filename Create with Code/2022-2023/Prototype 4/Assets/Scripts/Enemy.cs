using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    public float speed = 3f;
    private float abyssY = -10;
    public float rocketImpactFactor = 1f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = player.transform.position - transform.position;
        Vector3 lookDirection = delta.normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (transform.position.y < abyssY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rocket"))
        {
            // Destroys rocket
            Destroy(other.gameObject);

            // Applies impulse to self            
            enemyRb.AddForce(
                other.gameObject.transform.up * rocketImpactFactor, 
                ForceMode.Impulse);
        }
    }
}
