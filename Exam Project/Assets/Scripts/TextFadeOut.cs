using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFadeOut : MonoBehaviour
{
    private float fadeDuration = 0.5f;
    private float verticalSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Randomly shifts the text so that if 2 blocks are hit at once we see
        // 2 distinct labels
        transform.position += Vector3.right * Random.Range(-4.5f, +4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // Vertically raises the label a tiny bit at each frame
        transform.position += Vector3.up * verticalSpeed * Time.deltaTime;

        // Fades out the label
        TextMesh textMesh = GetComponent<TextMesh>();
        textMesh.color = new Color(
            textMesh.color.r, // Same red
            textMesh.color.g, // Same green
            textMesh.color.b, // Same blue            
            textMesh.color.a - (Time.deltaTime / fadeDuration) // Lower alpha (more and more transparent)
            );

        // When the label is completely transparent, removes it
        if (textMesh.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
