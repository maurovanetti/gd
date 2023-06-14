using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPaddle : MonoBehaviour
{
    public float maxSpeed;
    public float maxRotationSpeed;
    public Space relativeTo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * maxSpeed, relativeTo);

        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back, horizontalInput * Time.deltaTime * maxRotationSpeed);
    }
}
