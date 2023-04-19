using UnityEngine;

public class ColorChanger : MonoBehaviour

{
    private SpriteRenderer spriteRenderer;
    private bool isCyan = true;

    void Start()
    {
        // Get the SpriteRenderer component of the object
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Check if space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Check the current color and set the new color
            if (isCyan)
            {
                spriteRenderer.color = Color.cyan;
            }
            else
            {
                spriteRenderer.color = Color.red;
            }

            // Toggle the color flag
            isCyan = !isCyan;
        }
    }
}