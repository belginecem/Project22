using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMagneticController : MonoBehaviour
{
    public static CharacterMagneticController characterMagneticController;
   // MagneticObject[] magnets;
   //FerromagneticObject[] ferromagnetics;
    public bool debugGizmosActive = false;
    public bool _active = false;
    private FerromagneticObstecle[] ferromagneticsObs;
    private CharacterMagnet[] _characterMagnets;
    

    private void Start()
    {
        if (characterMagneticController == null) characterMagneticController = this;
        else Destroy(gameObject);
        //magnets = FindObjectsOfType<MagneticObject>();
        //ferromagnetics = FindObjectsOfType<FerromagneticObject>();
        ferromagneticsObs = FindObjectsOfType<FerromagneticObstecle>();
        _characterMagnets = FindObjectsOfType<CharacterMagnet>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            _active = true;
        if (Input.GetKeyDown(KeyCode.R))
            _active = false;
    }

    private void FixedUpdate()
    {
        if (ferromagneticsObs.Length > 0)
        {
            foreach (FerromagneticObstecle ferromagnetic in ferromagneticsObs)
            {
                if (ferromagnetic.enabled)
                {
                    foreach (CharacterMagnet magnet in _characterMagnets)
                    {
                        if (magnet.enabled && _active)
                        {
                            Vector3 dir = magnet.GetAttractionDirection2(ferromagnetic.transform.position);
                            float distance = dir.magnitude;
                            if (distance < magnet.attractionDistance2)
                            {
                                ferromagnetic.GetComponent<Rigidbody2D>().AddForce(dir.normalized * (magnet.attractionDistance2-distance) * magnet.attractionPower2);
                                if (debugGizmosActive) Debug.DrawLine(magnet.transform.position, ferromagnetic.transform.position, Color.green);
                            }
                            else
                            {
                                if (debugGizmosActive) Debug.DrawLine(magnet.transform.position, ferromagnetic.transform.position, Color.red);
                            }
                        }
                    }
                }
            }
        }
    }
}