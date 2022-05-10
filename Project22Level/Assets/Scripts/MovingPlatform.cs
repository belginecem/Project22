using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;

    private int i; //index of the array

    private void Start()
    {
        transform.position = points[startingPoint].position;//starting position of the platform to 
                                                            // the position of the one of the points using index "startingPoint"
    }

    private void Update()
    {
        // checking the distance of the platform and the point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++; //increase the index
            if(i == points.Length) //check if the platform was on the last point after the index increase
            {
                i = 0;//reset the index
            }
        }
        //moving the platform to the point position with the index "i"
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

       //Player platform üstünde hareket edebilsin
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
