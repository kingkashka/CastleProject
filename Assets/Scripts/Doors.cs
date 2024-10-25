using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{
    [SerializeField] float loadTime = 2f;

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
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        if (DoorCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            bool isInteractingWithDoor = CrossPlatformInputManager.GetButtonDown("Vertical");
            if (isInteractingWithDoor)
            {
                DoorAnimator.SetTrigger("Open");
                StartCoroutine(LoadLevel());
            }
        }
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(loadTime);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);  
    }
}
