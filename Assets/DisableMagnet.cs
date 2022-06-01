using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMagnet : MonoBehaviour
{

    public string CharacterMagnet;

    void Start()
    {
        GetComponent<CharacterMagnet>().enabled = false; //disables the magnetic feature at start
    }


}

