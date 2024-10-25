using System.Collections;
using UnityEngine;

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
        EnemyMovement();
    }

    private void EnemyMovement()
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

    public void PigDeath()
    {
        pigAnimator.SetTrigger("pigDeath");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        pigRigidBody.bodyType = RigidbodyType2D.Static;
        StartCoroutine(destroyPig());
    }

    IEnumerator destroyPig()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
