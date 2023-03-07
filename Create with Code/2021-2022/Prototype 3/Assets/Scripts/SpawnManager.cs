using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    private float startDelay = 100;
    private float repeatRate = 2;
    private Vector3 spawnPos = new Vector3(20, 0, 0);
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false && obstaclePrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject obstacle = obstaclePrefabs[randomIndex];
            Instantiate(obstacle, spawnPos, obstacle.transform.rotation);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
