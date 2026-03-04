using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
   

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float doubleJumpForce = 6f; // 二段跳力度，可调整

    private bool canDoubleJump = true; // 二段跳开关
    private enum MovementState { idle, running, jumping, falling, doublejumping };
    [SerializeField] private AudioSource jumpSoundEffect;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // 检测是否在地面上，如果是则重置二段跳
        if (Isgrounded())
        {
            canDoubleJump = true;
        }

        // 跳跃逻辑
        if (Input.GetButtonDown("Jump"))
        {
            if (Isgrounded())
            {
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (canDoubleJump)
            {
                jumpSoundEffect.Play();
                canDoubleJump = false;
                rb.velocity = new Vector2(rb.velocity.x, 0); // 重置Y轴速度，使二段跳更可控
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
            }
        }

        UpdateAnimationState();
    }

    // ... existing code ...
    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        // 优先判断是否正在执行二段跳
        if (!Isgrounded() && !canDoubleJump && rb.velocity.y > 0f)
        {
            state = MovementState.doublejumping;
        }
        // 然后判断是否是普通跳跃
        else if (rb.velocity.y > 0f && !Isgrounded())
        {
            state = MovementState.jumping;
        }
        // 最后判断是否是下落
        else if (rb.velocity.y < 0f && !Isgrounded())
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }
    // ... existing code ...

    public  bool Isgrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}