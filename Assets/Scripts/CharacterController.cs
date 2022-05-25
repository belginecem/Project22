using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float walkSpeed;
    private float moveInput;
    public bool isGrounded;
    private Rigidbody2D rb;
    public LayerMask groundMask;
    private Collider2D _collider;
    private Vector2 _respawnPoint;

    public PhysicsMaterial2D bounceMat, normalMat;

    public bool canJump = true;
    public float jumpValue = 0.0f;

    [SerializeField] private bool _active = true;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _collider = rb.GetComponent<Collider2D>();
        SetRespawnPoint(transform.position);
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if(jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
        }


        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.4f), 0f, groundMask);

        if(jumpValue > 0)
        {
            rb.sharedMaterial = bounceMat; 
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }

        if (Input.GetKey("space") && isGrounded && canJump)
        {
            jumpValue += 0.035f; //jump value increase
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        /*if(Input.GetKeyDown("space") && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }*/

        if(jumpValue >= 14f && isGrounded) //after 18f it jumpes otomatically
        {
            float tempx = moveInput * walkSpeed; //the arc on x direction
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
        }

        if (Input.GetKeyUp("space")) //jumps on release
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(moveInput * walkSpeed, jumpValue);
                jumpValue = 0.0f;
            }
            canJump = true;
        }

    }




    void ResetJump()
    {
        canJump = false;
        jumpValue = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.2f));
    }

    //private void MiniJump()
    //{
    //   rb.velocity = new Vector2(rb.velocity.x, jumpValue/2);
    //}

    public void SetRespawnPoint(Vector2 position)
    {
        _respawnPoint = position;
    }
    public void Die()
    {
        _active = false;
        _collider.enabled = false;
        //MiniJump();
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.4f);
        transform.position = _respawnPoint;
        _active = true;
        _collider.enabled = true;
        //MiniJump();
    }
}
