using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform playerTransform;
    public float offset; //distance between the player and character



    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void LateUpdate()
    {
        //camera's current position
        Vector3 temp = transform.position;

        //set the camera's position to the character's position
        temp.x = playerTransform.position.x;

        //add the offset value to the temporary camera position 
        temp.x += offset;

        //camera's temp posiiton is set to camera's current position
        transform.position = temp;
    }
}
