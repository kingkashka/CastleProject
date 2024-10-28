using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{
    [SerializeField] float loadTime = 2f;
    [SerializeField] AudioClip openingDoorSFX, closingDoorSFX;

    Rigidbody2D DoorRigidBody;
    Animator DoorAnimator;
    BoxCollider2D DoorCollider;
    AudioClip myAudioSource;




    // Start is called before the first frame update
    void Start()
    {
        DoorRigidBody = GetComponent<Rigidbody2D>();
        DoorAnimator = GetComponent<Animator>();
        DoorCollider = GetComponent<BoxCollider2D>();
        myAudioSource = GetComponent<AudioClip>();
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
                DoorAnimator.SetTrigger("DoorEnter");
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

    void PlayOpeningSound()
    {
        AudioSource.PlayClipAtPoint(openingDoorSFX, Camera.main.transform.position);
    }
    void PlayClosingSound()
    {
        AudioSource.PlayClipAtPoint(closingDoorSFX, Camera.main.transform.position);
    }
}
