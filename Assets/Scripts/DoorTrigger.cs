using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
   [SerializeField] private DoorAnimated door;
   [SerializeField] private DoorSetActive destroy;

/*
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.O))
      {
         door.OpenDoor();
         destroy.OpenDoor();
      }
      
      if (Input.GetKeyDown(KeyCode.K))
      {
         door.CloseDoor();
         destroy.CloseDoor();
      }
   }*/

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.gameObject.CompareTag("Player"))
      {
         Debug.Log("Button algılandı.");
         
      }
      
      if (col.CompareTag("Player"))
      {
         door.OpenDoor();
         destroy.OpenDoor();
         
         Debug.Log("Button algılandı.");
      }
      else 
      {
         door.CloseDoor();
         destroy.CloseDoor();
      }
   }
   
}
