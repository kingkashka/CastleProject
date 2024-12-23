using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 3f;
    [SerializeField] float jumpSpeed = 2f;
    [SerializeField] float climbSpeed = 1.0f;
    [SerializeField] float attackRadius = 3f;
    [SerializeField] Vector2 hitKick = new Vector2(1f, 1f);
    [SerializeField] Transform hurtBox;
    [SerializeField] public AudioClip  jumpingSFX, attackingSFX, hittingSFX, deathSFX;


    Rigidbody2D myRigidBody;
    public Animator myAnimator;
    BoxCollider2D myBoxCollider;
    PolygonCollider2D myPlayerFeet;
    PolygonCollider2D myPlayersHands;
    AudioSource myAudioSource;


    float startingGravityScale;
    public bool isHurting = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myPlayerFeet = GetComponent<PolygonCollider2D>();
        myPlayersHands = GetComponent<PolygonCollider2D>();
        myAudioSource = GetComponent<AudioSource>();

        startingGravityScale = myRigidBody.gravityScale;

        myAnimator.SetTrigger("DoorExit");
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
            Attacking();

            if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                PlayerHit();
            }

            ExitLevel();
        }

    }

    private void ExitLevel()
    {
        bool isTouching = myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Doors"));
        if (isTouching)
        {
            return;
        }
        if (CrossPlatformInputManager.GetButtonDown("Vertical") && isTouching)
        {
            myAnimator.SetTrigger("DoorEnter");
            FindObjectOfType<Doors>().LoadNextLevel();
        }
    }
    public void LoadNextLevel()
    {
        FindObjectOfType<Doors>().LoadNextLevel();
        turnOffRender();
    }
    public void turnOffRender()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hurtBox.position, attackRadius);
    }
    private void Attacking()
    {
        bool isAttacking = CrossPlatformInputManager.GetButtonDown("Fire3");
        if (isAttacking)
        {
            myAnimator.SetTrigger("Attacking");
            Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(hurtBox.position, attackRadius, LayerMask.GetMask("Enemy"));
            myAudioSource.PlayOneShot(attackingSFX);

            foreach (Collider2D enemy in enemiesToHit)
            {
                enemy.GetComponent<EnemyPig>().PigDeath();
            }
        }
    }

    public void PlayerHit()
    {
        myRigidBody.velocity = hitKick * new Vector2(-transform.localScale.x, 5f);
        myAnimator.SetTrigger("Hitting");
        isHurting = true;
        myAudioSource.PlayOneShot(hittingSFX);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
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
        bool isJumping = CrossPlatformInputManager.GetButtonDown("Fire1");
        if (isJumping)
        {
            Vector2 jumpVelocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
            myRigidBody.velocity = jumpVelocity;

            myAudioSource.PlayOneShot(jumpingSFX);
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
