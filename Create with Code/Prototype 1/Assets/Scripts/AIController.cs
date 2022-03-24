using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float speed;
    public Transform spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnLocation)
        {
            transform.position = spawnLocation.position;
            transform.rotation = spawnLocation.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.y < -20)
        {
            if (spawnLocation)
            {
                transform.position = spawnLocation.position;
                transform.rotation = spawnLocation.rotation;
            }
        }
    }
}
