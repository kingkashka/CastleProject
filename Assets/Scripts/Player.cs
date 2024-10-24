using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 3f;
    [SerializeField] float jumpSpeed = 2f;
    [SerializeField] float climbSpeed = 1.0f;
    [SerializeField] Vector2 hitKick = new Vector2(1f, 1f);

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    BoxCollider2D myBoxCollider;
    PolygonCollider2D myPlayerFeet;
    PolygonCollider2D myPlayersHands;


    float startingGravityScale;
    bool isHurting = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myPlayerFeet = GetComponent<PolygonCollider2D>();
        myPlayersHands = GetComponent<PolygonCollider2D>();

        startingGravityScale = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHurting)
        {
            Run();
            IdleState();
            Jump();
            Climb();

            if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                PlayerHit();
            }
        }

    }

    public void PlayerHit()
    {
        myRigidBody.velocity = hitKick * new Vector2(-transform.localScale.x, 1f);
        myAnimator.SetTrigger("Hitting");
        isHurting = true;
        StartCoroutine(stopHurting());
    }

    IEnumerator stopHurting()
    {
        yield return new WaitForSeconds(1f);

        isHurting = false;
    }
    private void Climb()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Climbables")))
        {
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
            myRigidBody.velocity = climbVelocity;

            myAnimator.SetBool("Climbing", true);

            myRigidBody.gravityScale = 0f;
        }
        else
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = startingGravityScale;
        }

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
