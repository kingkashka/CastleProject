using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player2 : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    Rigidbody2D player2RigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player2RigidBody = GetComponent<Rigidbody2D>();
    }

    private void Run()
    {
        float controlThrow2 = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 player2Velocity = new Vector2(controlThrow2 * runSpeed, player2RigidBody.velocity.y);
        player2RigidBody.velocity = player2Velocity;
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
