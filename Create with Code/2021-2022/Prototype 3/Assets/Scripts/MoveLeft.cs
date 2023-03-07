using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed;
    private const float slowSpeed = 10;
    private const float quickSpeed = 20;
    private float leftBound = -15;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    private void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerControllerScript.isDashing)
        {
            speed = quickSpeed;
        }
        else
        {
            speed = slowSpeed;
        }

        if (playerControllerScript.intro == false && playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        
    }
}
