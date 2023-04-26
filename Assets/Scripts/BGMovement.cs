using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{

    public float scrollSpeed = 0.1f;
    public float tileSize = 20f;

    private Transform playerTransform;
    private Vector3 lastPlayerPosition;
    private float textureOffsetX;
    private float textureOffsetY;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lastPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        Vector3 movement = playerTransform.position - lastPlayerPosition;
        textureOffsetX += movement.x * scrollSpeed / tileSize;
        textureOffsetY += movement.y * scrollSpeed / tileSize;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(textureOffsetX, textureOffsetY);
        lastPlayerPosition = playerTransform.position;

        // Update camera position to follow player
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = playerTransform.position.x;
        cameraPosition.y = playerTransform.position.y;
        transform.position = cameraPosition;
    }
}