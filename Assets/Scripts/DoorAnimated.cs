using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimated : MonoBehaviour
{
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        _animator.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        _animator.SetBool("Open", false);
    }
}
