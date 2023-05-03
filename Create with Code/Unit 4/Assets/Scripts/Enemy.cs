using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2.0f;
    private Rigidbody enemyRb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = player.transform.position;
        Vector3 e = transform.position;
        Vector3 direction = (p - e).normalized;
        Vector3 push = direction * speed;
        enemyRb.AddForce(push);
    }
}
