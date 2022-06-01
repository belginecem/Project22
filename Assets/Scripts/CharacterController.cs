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
    public Animator animator;

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
            
            //walking animation
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
            
            //walking animation turn rotation
            if (rb.velocity.x > 0.0f)
            {
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }

            else if (rb.velocity.x < 0.0f)
            {
                transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
            }
            

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
            jumpValue += 0.16f; //jump value increase
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            animator.Play("player_jump");
        }

        

        if(jumpValue >= 20 && isGrounded) //after 20 it jumpes otomatically
        {
            float tempx = moveInput * walkSpeed; //the arc on x direction
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
            animator.Play("player_jump");

        }

        if (Input.GetKeyUp("space")) //jumps on release
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(moveInput * walkSpeed, jumpValue);
                jumpValue = 0.0f;
                animator.Play("player_jump2");
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
    //   rb.velocity = new Vector2(rb.velocity.x, jumpValue - 6f);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            GetComponent<CharacterMagnet>().enabled = true; //enables the magnetic feature after upgrade
        }
    }
}
