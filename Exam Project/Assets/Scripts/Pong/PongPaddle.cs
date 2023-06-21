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
        float zRotation = transform.rotation.eulerAngles.z;
        if (zRotation > 70 && zRotation <= 180 && horizontalInput < 0)
        {
            horizontalInput = 0;
        } 
        else if (zRotation > 180 && zRotation < 290 && horizontalInput > 0)
        {
            horizontalInput = 0;
        }
        transform.Rotate(Vector3.back, horizontalInput * Time.deltaTime * maxRotationSpeed);
    }
}
