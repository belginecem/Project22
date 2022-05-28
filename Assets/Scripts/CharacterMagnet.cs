using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMagnet : MonoBehaviour
{
    public float attractionDistance2;
    public float attractionPower2;
    

    
    
    public Vector3 GetAttractionDirection2(Vector3 position)
    {
        return (transform.position - position);
    }
    private void OnDrawGizmos()
    {
        if (CharacterMagneticController.characterMagneticController.debugGizmosActive) Gizmos.DrawWireSphere(transform.position, attractionDistance2);
        
    }
    
     
}
