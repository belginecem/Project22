using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<CharacterController>();
        if (player != null)
        {
            player.SetRespawnPoint(transform.position);
        }
    }
}
