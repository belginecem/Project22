using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) { 
        if (collision.gameObject.CompareTag("Water")) {
            Destroy(gameObject);
            LevelManager.instance.Respawn();
        }
    }
}
