using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 cursorHotspot = Vector2.zero;

    private void Start()
    {
#if UNITY_WEBGL
        float xspot = cursorTexture.width / 2;
        float yspot = cursorTexture.height / 2;
        Vector2 hotSpot = new Vector2(xspot, yspot);
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);
#else
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
#endif

    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        transform.position = mousePosition;

    }

}

