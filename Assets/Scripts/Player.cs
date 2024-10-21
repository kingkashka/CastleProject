using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 3f;
    [SerializeField] float jumpSpeed = 2f;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    BoxCollider2D myBoxCollider;
    PolygonCollider2D myPlayerFeet;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myPlayerFeet = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        IdleState();
        Jump();
    }

    private void Jump()
    {

        if (!myPlayerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        bool isJumping = CrossPlatformInputManager.GetButtonDown("Jump");
        if (isJumping)
        {
            Vector2 jumpVelocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
            myRigidBody.velocity = jumpVelocity;
        }
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        flipSprite();

        myAnimator.SetBool("Running", true);

        print(playerVelocity);


    }
    void IdleState()
    {
        bool runningHorizontally = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (!runningHorizontally)
        {
            myAnimator.SetBool("Running", false);
        }
    }
    private void flipSprite()
    {
        bool runningHorizontally = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (runningHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

}
