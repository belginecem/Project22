using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMagnet : MonoBehaviour
{

    public string CharacterMagnet;
    public string FerromagneticObject;

    void Start()
    {
        GetComponent<CharacterMagnet>().enabled = false; //disables the magnetic feature at start - E/R
        GetComponent<FerromagneticObject>().enabled = false; //disables the magnetic feature at start - F/G
    }


}

