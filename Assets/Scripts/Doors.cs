using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Doors : MonoBehaviour
{
    Rigidbody2D DoorRigidBody;
    Animator DoorAnimator;
    BoxCollider2D DoorCollider;




    // Start is called before the first frame update
    void Start()
    {
        DoorRigidBody = GetComponent<Rigidbody2D>();
        DoorAnimator = GetComponent<Animator>();
        DoorCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        OpenCloseDoor();
    }

    private void OpenCloseDoor()
    {
        if (DoorCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            bool isInteractingWithDoor = CrossPlatformInputManager.GetButtonDown("Fire1");
            if (isInteractingWithDoor)
            {
                DoorAnimator.SetTrigger("DoorInteracting");
                //GetComponent<Player>().myAnimator.SetTrigger("Door Enter");
            }
        }
    }
}
