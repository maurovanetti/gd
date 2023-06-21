using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongEnemyPaddle : MonoBehaviour
{
    public float strength;
    public float repulsionFromWalls;
    public Transform ballTransform;
    private Rigidbody enemyRb;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float dy = ballTransform.position.y - transform.position.y;
        if (dy > 10) // ball is well above it
        {
            enemyRb.AddForce(strength * Vector3.up, ForceMode.Force);
        }
        else if (dy < -10) // ball is well below it
        {
            enemyRb.AddForce(strength * Vector3.down, ForceMode.Force);
        }
        else // ball is almost at the same height
        {
            // Does nothing
        }
        if (transform.position.y > 40)
        {
            enemyRb.AddForce(repulsionFromWalls * Vector3.down, ForceMode.Force);
        }
        else if (transform.position.y < 10)
        {
            enemyRb.AddForce(repulsionFromWalls * Vector3.up, ForceMode.Force);
        }
    }
}
