using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    
    public float speed = 10.4f;
    private Vector2 screenBounds;
    private float objWidth;
    private float objHeight;


       

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = transform.position;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //if (viewPos.x > (screenBounds.x * -1) + objWidth)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }

        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //if (viewPos.x < screenBounds.x - objWidth)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }

        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            //if (viewPos.y < (screenBounds.y * 1) - objHeight)
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }

        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            // if (viewPos.y > (screenBounds.y * -1) + objHeight)
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
            }

        }

    }
}
