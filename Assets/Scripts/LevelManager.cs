using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform respawnPoint;
    public GameObject CharacterPrefab;

    public CinemachineVirtualCameraBase cam;

    private void Awake()
    {
        instance = this;
    }

    public void Respawn()
    {
        GameObject player = Instantiate(CharacterPrefab, respawnPoint.position, Quaternion.identity);
        cam.Follow = player.transform;
    }
}