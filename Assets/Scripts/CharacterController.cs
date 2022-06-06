using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float walkSpeed;
    private float moveInput;
    public bool isGrounded;
    public bool isJumping;
    private Rigidbody2D rb;
    public LayerMask groundMask;
    private Collider2D _collider;
    private Vector2 _respawnPoint;
    public SpriteRenderer _sprenderer;

    public PhysicsMaterial2D bounceMat, normalMat;
    public Animator animator;

    private bool jump; //for animation
    private bool grounded = true; //for animation
    private bool onAir = false;

    public bool canJump = true;
    public float jumpValue = 0.0f;

    [SerializeField] private bool _active = true;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _collider = rb.GetComponent<Collider2D>();
        SetRespawnPoint(transform.position);
        _sprenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");


        if (jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);

            //walking animation
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
            
            //walking animation turn rotation
            if (Input.GetAxis("Horizontal") > 0.01f)
            {
                _sprenderer.flipX = true;
            }
            else if (Input.GetAxis("Horizontal") < -0.01f)
            {
                _sprenderer.flipX = false;
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
            jumpValue += 0.060f; //jump value increase
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            //animator.SetBool("isJumping", true);
            //animator.Play("player_jump");
            jump = true;
            grounded = false;
            animator.SetTrigger("jump");
            animator.SetBool("grounded", false);
        }

        

        if(jumpValue >= 20 && isGrounded) //after 20 it jumpes otomatically
        {
            float tempx = moveInput * walkSpeed; //the arc on x direction
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
            //animator.SetBool("isJumping", true);
            //animator.Play("player_jump");
            jump = true;
            grounded = false;
            animator.SetTrigger("jump");
            animator.SetBool("grounded", false);

        }

        if (Input.GetKeyUp("space")) //jumps on release
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(moveInput * walkSpeed, jumpValue);
                jumpValue = 0.0f;
                //animator.SetBool("isJumping", true);
                //animator.Play("player_jump");
                animator.SetBool("onAir", true);
            }
            canJump = true;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("grounded", true);
            grounded = true;
            animator.SetBool("onAir", false);
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
        FindObjectOfType<AudioManager>().Play("Death");
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
            GetComponent<FerromagneticObject>().enabled = true;
            FindObjectOfType<AudioManager>().Play("PowerUp");
        }
        if (other.gameObject.CompareTag("UI"))
        {
            var a = other.gameObject.transform.GetChild(0).gameObject;
            a.SetActive(true);
        }
        if (other.gameObject.CompareTag("TheEnd"))
        {
            var a = other.gameObject.transform.GetChild(0).gameObject;
            a.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("UI"))
        {
            var a = other.gameObject.transform.GetChild(0).gameObject;
            a.SetActive(false);
        }
        if (other.gameObject.CompareTag("TheEnd"))
        {
            var a = other.gameObject.transform.GetChild(0).gameObject;
            a.SetActive(false);
        }
    }
}
