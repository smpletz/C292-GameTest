using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    private float Move;

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
    [SerializeField] float stamina;
    private bool isDraining;
    private bool isWallJumping;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        if(!isWallSliding && !isWallJumping)
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

    private void StopWallJump()
    {
        isWallJumping = false;
    }

    private void DrainStamina()
    {
        stamina -= .1f;
        isDraining = false;
    }
}
