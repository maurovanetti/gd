using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public string playerGameObjectName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collidingThing)
    {
        Destroy(gameObject);
        if (collidingThing.gameObject.name == playerGameObjectName) {
            PlayerController playerController;
            playerController = collidingThing.gameObject.GetComponent<PlayerController>();
            playerController.lives--;            
            if (playerController.lives <= 0) {
                Debug.LogWarning("Game Over: Crushed by the stampede");
            }
        } else {
            Destroy(collidingThing.gameObject);
        }        
    }
}
