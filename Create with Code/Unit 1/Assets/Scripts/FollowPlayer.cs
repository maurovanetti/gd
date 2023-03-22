using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // The object we want to follow
    public GameObject player;

    // The offset between player and its follower
    private Vector3 offset = new Vector3(0, 5, -7);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // LateUpdate is called every frame after all Update calls
    void LateUpdate()
    {
        // Keeps this object at offset from player
        transform.position = player.transform.position + offset;
    }
}
