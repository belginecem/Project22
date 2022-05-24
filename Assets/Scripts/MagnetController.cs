using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    public static MagnetController magnetController;
    MagneticObject[] magnets;
    FerromagneticObject[] ferromagnetics;
    public bool debugGizmosActive = false;
    public bool _active = false;

    private void Start()
    {
        if (magnetController == null) magnetController = this;
        else Destroy(gameObject);
        magnets = FindObjectsOfType<MagneticObject>();
        ferromagnetics = FindObjectsOfType<FerromagneticObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _active = true;
        if (Input.GetKeyDown(KeyCode.G))
            _active = false;
    }

    private void FixedUpdate()
    {
        if (ferromagnetics.Length > 0)
        {
            foreach (FerromagneticObject ferromagnetic in ferromagnetics)
            {
                if (ferromagnetic.enabled)
                {
                    foreach (MagneticObject magnet in magnets)
                    {
                        if (magnet.enabled && _active)
                        {
                            Vector3 dir = magnet.GetAttractionDirection(ferromagnetic.transform.position);
                            float distance = dir.magnitude;
                            if (distance < magnet.attractionDistance)
                            {
                                ferromagnetic.GetComponent<Rigidbody2D>().AddForce(dir.normalized * (magnet.attractionDistance-distance) * magnet.attractionPower);
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
