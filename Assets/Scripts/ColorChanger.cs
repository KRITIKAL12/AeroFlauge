using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour

{
    private SpriteRenderer spriteRenderer;
    private bool isCyan = true;

    private bool isBlinking = false;

    public float blinkDuration = 3.0f;
    public float blinkInterval = 0.2f;


    void Start()
    {
        // Get the SpriteRenderer component of the object
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(BlinkPlayer());
    }

    private IEnumerator BlinkPlayer()
    {
        isBlinking = true;
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            yield return new WaitForSeconds(blinkInterval);

            // Toggle the player's color between red and cyan
            if (spriteRenderer.color == Color.red)
            {
                spriteRenderer.color = Color.cyan;
            }
            else
            {
                spriteRenderer.color = Color.red;
            }

            elapsedTime += blinkInterval;
        }

        // Reset the color to the original color after blinking stops
        spriteRenderer.color = Color.red;
        isBlinking = false;
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