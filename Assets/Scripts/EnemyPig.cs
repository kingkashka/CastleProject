using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class EnemyPig : MonoBehaviour
{
    [SerializeField] float pigRunSpeed = 3f;

    Rigidbody2D pigRigidBody;
    Animator pigAnimator;


    // Start is called before the first frame update
    void Start()
    {
        pigRigidBody = GetComponent<Rigidbody2D>();
        pigAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isFacingLeft())
        {
            pigRigidBody.velocity = new Vector2(-pigRunSpeed, 0f);
        }
        else
        {
            pigRigidBody.velocity = new Vector2(pigRunSpeed, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        flipSprite();
    }
    private void flipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(pigRigidBody.velocity.x), 1f);
    }
    private bool isFacingLeft()
    {
        return transform.localScale.x > 0f;
    }
    
    //private void idleState()
    //{
    //    bool runningSideways = Mathf.Abs(pigRigidBody.velocity.x) > Mathf.Epsilon;
    //    if (!runningSideways)
    //    {
    //        pigAnimator.SetBool("Running", false);
    //    }
    //}
    //private void Run()
    //{
    //    pigRigidBody.velocity = new Vector2(-pigRunSpeed, 0f);
        
    //    flipSprite();

    //    pigAnimator.SetBool("Running", true);
    //}
    
}
