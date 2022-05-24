using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticObject : MonoBehaviour
{
    public float attractionDistance;
    public float attractionPower;
    

    
    
    public Vector3 GetAttractionDirection(Vector3 position)
    {
        return (transform.position - position);
    }
    private void OnDrawGizmos()
    {
        if (MagnetController.magnetController.debugGizmosActive) Gizmos.DrawWireSphere(transform.position, attractionDistance);
        
    }
}
