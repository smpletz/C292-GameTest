using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player playerInstance;
    public Animator animator;

    [SerializeField] float speed;
    private float Move;
    private float Vert;

    [SerializeField] float jump;

    [SerializeField] Transform foot;
    [SerializeField] Transform sideTop;
    [SerializeField] Transform sideMid;
    [SerializeField] Transform sideBottom;
    [SerializeField] LayerMask groundMask;

    private bool isWallSliding;
    [SerializeField] float wallSlidingSpeed;
    [SerializeField] float wallClimbingSpeed;
    [SerializeField] float wallJumpForce;
    [SerializeField] float wallJumpDuration;
    private float stamina;
    private bool isDraining;
    private bool isWallJumping;

    [SerializeField] float dashForce;
    private bool isDashing;
    public bool hasDash;

    private Rigidbody2D rb;

    // Awake is called before Start
    private void Awake()
    {
        playerInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDraining = false;
        isWallJumping = false;
        isWallSliding = false;
        hasDash = true;
        isDashing = false;
        stamina = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");
        Vert = Input.GetAxis("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(Move));
        animator.SetFloat("Vert", rb.velocity.y);
        animator.SetBool("WallSlide", isWallSliding);
        animator.SetBool("Dashing", isDashing);
        animator.SetBool("Dead", GameManager.instance.GetPlayerDead());

        if(!isWallSliding && !isWallJumping && !isDashing)
        {
            rb.velocity = new Vector2(speed * Move, rb.velocity.y);
        }

        // Standard Jump
        if(Input.GetButtonDown("Jump") && GroundCheck())
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }

        // Wall Jump
        if(Input.GetButtonDown("Jump") && isWallSliding)
        {
            isWallJumping = true;
            rb.AddForce(new Vector2(wallJumpForce * -Move, jump));
            Invoke("StopWallJump", wallJumpDuration);
        }

        // Wall Climbing
        if(Input.GetKey(KeyCode.UpArrow) && isWallSliding && stamina > 0)
        {
            rb.velocity = new Vector2(0,wallClimbingSpeed);
            if(!isDraining)
            {
                Invoke("DrainStamina", .1f);
                isDraining = true;
            }
        }

        // Dash
        if(Input.GetButtonDown("Fire3") && hasDash)
        {
            hasDash = false;
            isDashing = true;
            if(Input.GetKey(KeyCode.RightArrow))
            {
                if(Input.GetKey(KeyCode.UpArrow))
                {
                    rb.velocity = new Vector2(dashForce, dashForce);
                }
                else if(Input.GetKey(KeyCode.DownArrow))
                {
                    rb.velocity = new Vector2(dashForce, -dashForce);
                }
                else
                {
                    rb.velocity = new Vector2(1.5f * dashForce, 0);
                }
            }
            else if(Input.GetKey(KeyCode.LeftArrow))
            {
                if(Input.GetKey(KeyCode.UpArrow))
                {
                    rb.velocity = new Vector2(-dashForce, dashForce);
                }
                else if(Input.GetKey(KeyCode.DownArrow))
                {
                    rb.velocity = new Vector2(-dashForce, -dashForce);
                }
                else
                {
                    rb.velocity = new Vector2(-1.5f * dashForce, 0);
                }
            }
            else
            {
                if(Input.GetKey(KeyCode.UpArrow))
                {
                    rb.velocity = new Vector2(0, 1.5f * dashForce);
                }
                else if(Input.GetKey(KeyCode.DownArrow))
                {
                    rb.velocity = new Vector2(0, -1.5f * dashForce);
                }
                else
                {
                    hasDash = true;
                    isDashing = false;
                }
            }
            
            if(isDashing)
            {
                Invoke("EndDash", .5f);
            }
        }

        // Dash replenish when touching ground
        if(GroundCheck())
        {
            hasDash = true;
            stamina = 10f;
        }

        Flip();

        WallSlide();
    }

    // Changes Player direction to match movement direction
    void Flip()
    {
        if (Move < -0.01f)
        {
            transform.localScale = new Vector2(-1,1);
        }
        if (Move > 0.01f)
        {
            transform.localScale = new Vector2(1,1);
        }
    }

    // Checks if Player is in contact with ground
    private bool GroundCheck()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(foot.position, Vector2.down, .2f, groundMask);
 
        return hit;
    }

    // Checks if Player is in contact with a wall (also under layer "ground")
    private bool WallCheck()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(sideTop.position, Vector2.right, .2f, groundMask);
        if(!hit)
        {
            hit = Physics2D.Raycast(sideMid.position, Vector2.right, .2f, groundMask);
        }
        if(!hit)
        {
            hit = Physics2D.Raycast(sideBottom.position, Vector2.right, .2f, groundMask);
        }
        if(!hit)
        {
            hit = Physics2D.Raycast(sideTop.position, Vector2.left, .2f, groundMask);
        }
        if(!hit)
        {
            hit = Physics2D.Raycast(sideMid.position, Vector2.left, .2f, groundMask);
        }
        if(!hit)
        {
            hit = Physics2D.Raycast(sideBottom.position, Vector2.left, .2f, groundMask);
        }

        return hit;
    }

    // Checks if Player is pressing into a wall
    private void WallSlide()
    {
        if(WallCheck() && !GroundCheck() && Move != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GameManager" || collision.gameObject.tag == "Spike")
        {
            GameManager.instance.InitatePlayerDead();
        }
    }

    private void StopWallJump()
    {
        isWallJumping = false;
    }

    private void EndDash()
    {
        isDashing = false;
    }

    private void DrainStamina()
    {
        stamina -= .1f;
        isDraining = false;
    }
}
