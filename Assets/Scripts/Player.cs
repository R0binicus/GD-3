using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    public bool isUp = false;
    public bool isDown = false;





    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        playerDirection = new Vector2(directionX, 0).normalized;

        if(Input.GetKey(KeyCode.W) && !(Input.GetKeyDown(KeyCode.S)))
        {
            gameObject.layer = LayerMask.NameToLayer("PlayerDown");
            //gameObject.tag = "PlayerDown";
        }
        else if(Input.GetKey(KeyCode.S) && !(Input.GetKeyDown(KeyCode.W)))
        {
            gameObject.layer = LayerMask.NameToLayer("PlayerUp");
            //gameObject.tag = "PlayerUp";
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
            //gameObject.tag = "Player";
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(playerDirection.x * playerSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Border")
        {
            Destroy(this.gameObject);
        }
        //else if(collision.tag == "Obstacle")
        //{
        //    Destroy(this.gameObject);
        //}    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ObjectUp" && gameObject.tag == "PlayerUp")
        {
            Physics2D.IgnoreCollision( collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        } else if(collision.gameObject.tag == "ObjectDown" && gameObject.tag == "PlayerDown")
        {
            Physics2D.IgnoreCollision( collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
 } 


}

    /*
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float walkAcceleration = 100f;
    private bool canJump = true;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource footsteps;

    //Jump king Stuff
    [SerializeField] private bool jumpKingJump = false;
    [SerializeField] private float jumpForce = 0.0f;
    [SerializeField] private float jumpSpeed = 30.0f;
    [SerializeField] private float maxJumpForce = 20.0f;

    private Animator anim;
    [SerializeField] private Collider2D feetCollider;
    private SpriteRenderer spriteRenderer;
    private Transform feet;

    //Mario Movement Stuff
    [SerializeField] private float minJumpHeight = 1f;
    [SerializeField] private float maxJumpHeight = 5f;
    private float maxJumpTime 
            //=> (maxJumpHeight - minJumpHeight) / mJumpForce; // Jumps without gravity
            => (-6f / gravity) * (Mathf.Sqrt((2f / 3f) * ((-gravity) * maxJumpHeight + 2 * mJumpForce * mJumpForce)) - mJumpForce); // Jumps with 0-max gravity
    private float mJumpForce => Mathf.Sqrt(2 * gravity * minJumpHeight);
    private float gravity => Physics2D.gravity.magnitude * gravityDefault;

    [SerializeField] private float jumpTime = 0;
    private float inputAxis = 8.0f;
    private float gravityDefault;

    //private bool grounded;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        feet = transform.Find("Feet");
        gravityDefault = rb.gravityScale;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if(jumpKingJump) //If jump king mode is true, use the jump king stuff, otherwise use mario jumping
        {
            if(jumpForce == 0.0f && IsGrounded())
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * walkSpeed, rb.velocity.y);
            }

            if(Input.GetKeyDown(KeyCode.Space) && IsGrounded() && canJump)
            {
                rb.velocity = new Vector2(0.0f, rb.velocity.y);
            }

            if(Input.GetKey(KeyCode.Space) && IsGrounded() && canJump && jumpForce < maxJumpForce)
            {
                jumpForce += jumpSpeed * Time.deltaTime;
            }
        }
        else //Mario Jumping
        {
            //if(Input.GetKey(KeyCode.Space))
            //{
            //    Jump();
            //}
            HorizontalMovement();
            if (IsGrounded())
            {
                GroundedMovement(); 
            }

            ApplyJumpGravity();
			
			
        }
        if ((rb.velocity != Vector2.zero) && (IsGrounded()))
        {
            anim.SetBool("walk", true);
            if (!footsteps.isPlaying)
            {
                footsteps.Play();
                    }
        } else
        {
            anim.SetBool("walk", false);
            footsteps.Stop();
        }
        
        if(horizontalInput != 0) spriteRenderer.flipX = horizontalInput < 0f;

        jumpForce = Mathf.Min(jumpForce, maxJumpForce);


        //when space key is released
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(IsGrounded())
            {
                rb.velocity = new Vector2(horizontalInput * walkSpeed, jumpForce);
                jumpForce = 0.0f;
            }
            canJump = true;
        }

        //when space key is released
        if(Input.GetKeyUp(KeyCode.J))
        {
            if(jumpKingJump)
            {
                jumpKingJump = false;

            }
            else
            {
                jumpKingJump = true;
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, walkSpeed);
    }

    private void ResetJump()
    {
        canJump = false;
        jumpForce = 0;
    }

    public bool IsGrounded()
    {
        var hit = Physics2D.BoxCast(feet.position, transform.localScale, 0f, Vector2.down, 0.1f, jumpableGround);
        return hit && hit.point.y <= feet.position.y && feetCollider.IsTouching(hit.collider);
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, inputAxis * walkSpeed, walkAcceleration * Time.deltaTime), rb.velocity.y);

        //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * walkSpeed, rb.velocity.y); OLD
    }

    private void GroundedMovement()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Jump();
            rb.velocity = new Vector2(rb.velocity.x, mJumpForce);
            rb.gravityScale = jumpTime > 0 ? 0 : rb.gravityScale;
            jumpTime = maxJumpTime;
        }
    }

    private void FixedUpdate()
    {
        if(!jumpKingJump)
        {
            /*Vector2 position = rb.position;
            position += velocity * Time.fixedDeltaTime;

            rb.MovePosition(position);

            if (jumpTime > 0f)
            {
                jumpTime = Mathf.Max(jumpTime - Time.fixedDeltaTime, 0f);
                rb.gravityScale = gravityDefault * (maxJumpTime - jumpTime) / maxJumpTime;
            }
        }
    }

    private void ApplyJumpGravity()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpTime = 0f;
            rb.gravityScale = gravityDefault;
        }
    }
}
*/