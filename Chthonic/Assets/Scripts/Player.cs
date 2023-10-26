using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    private float Move;

    [SerializeField] float jump;

    [SerializeField] Transform foot;
    [SerializeField] Transform side;
    [SerializeField] LayerMask groundMask;

    bool isWallSliding;
    [SerializeField] float wallSlidingSpeed;

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

        rb.velocity = new Vector2(speed * Move, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && GroundCheck())
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }
    }

    private bool GroundCheck()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(foot.position, Vector2.down, .2f, groundMask);

        return hit;
    }

    private bool WallCheck()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(side.position, Vector2.right, .2f, groundMask)

        if(!hit)
        {
            hit = Physics2D.Raycast(side.position, Vector2.left, .2f, groundMask)
        }

        return hit;
    }

    private void WallSlide()
    {
        if(WallCheck() && !GroundCheck() && rb.velocity.x != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue))
        }
        else
        {
            isWallSliding = false;
        }
    }

    
}
