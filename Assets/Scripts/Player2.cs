using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player2 : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 2f;

    Rigidbody2D player2RigidBody;
    Animator myAnimator;
    BoxCollider2D myBoxCollider;
    PolygonCollider2D myPolygonCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        player2RigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myPolygonCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Idle();
        Jump();
    }

    private void Jump()
    {
        if (!myPolygonCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        bool isJumping = CrossPlatformInputManager.GetButtonDown("Jump");
            if(isJumping)
        {
            Vector2 jumpVelocity = new Vector2(player2RigidBody.velocity.x, jumpSpeed);
            player2RigidBody.velocity = jumpVelocity;
        }
    }
    private void Idle()
    {
        bool runningHorizontally = Mathf.Abs(player2RigidBody.velocity.x) > Mathf.Epsilon;
        if (!runningHorizontally)
        {
myAnimator.SetBool("Running", false);
        }
    }
    private void Run()
    {
        float controlThrow2 = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 player2Velocity = new Vector2(controlThrow2 * runSpeed, player2RigidBody.velocity.y);
        player2RigidBody.velocity = player2Velocity;
        myAnimator.SetBool("Running", true);
        FlipSprite();
    }

    private void FlipSprite()
    {
        bool runningHorizontally = Mathf.Abs(player2RigidBody.velocity.x) > Mathf.Epsilon;
        if(runningHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(player2RigidBody.velocity.x), 1f);
        }
    }
}
