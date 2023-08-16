using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private float fHorizontalVelocity = 0f;

    private float buttonTime = 0.3f;
    private float jumpTime;
    private bool jumping;
    private float fGroundRemember = 0f;

    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private float fGroundRememberTime = 0.15f;
    //[SerializeField] private float fHorizontalAcceleration = 1f;
    [SerializeField] [Range(0, 1)] private float fHorizontalDampingBasic = 0.5f;
    //[SerializeField] [Range(0, 1)] private float fHorizontalDampingWhenStopping = 0.5f;
    //[SerializeField] [Range(0, 1)] private float fHorizontalDampingWhenTurning = 0.5f;

    private enum MovementState { idle, running, jumping, falling }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        fGroundRemember -= Time.deltaTime;

        float fHorizontalVelocity = rb.velocity.x;
        fHorizontalVelocity *= Input.GetAxisRaw("Horizontal");
        fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * moveSpeed);
        rb.velocity = new Vector2(fHorizontalVelocity, rb.velocity.y);

        //dirX = Input.GetAxisRaw("Horizontal");
        //rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if ( IsGrounded() )
        {
            fGroundRemember = fGroundRememberTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && (fGroundRemember > 0) )
        {
            jumpSoundEffect.Play();
            
            jumping = true;
            jumpTime = 0;
        }

        if ( jumping ) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) | jumpTime > buttonTime   ) 
        {
            jumping = false;
        }

        

        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {
        MovementState state;
        if (fHorizontalVelocity > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (fHorizontalVelocity < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if ( rb.velocity.y > .1f )
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f )
        {
            state = MovementState.falling;
        }


        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
