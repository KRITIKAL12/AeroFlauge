using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    private Vector3 CameraPosition;

    public float smoothTime = 0.25f;
    public GameObject target;
    private Vector3 velocity = Vector3.zero;


    [Header("Camera Settings")]
    public float CameraSpeed;

    // Start is called before the first frame update
    void Start()
    {

        CameraPosition = this.transform.position; 

    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, -10f);

        this.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        /*if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
        {
            CameraPosition.x += CameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            CameraPosition.x -= CameraSpeed * Time.deltaTime;
        }*/

        //this.transform.position = CameraPosition;
    }
}
