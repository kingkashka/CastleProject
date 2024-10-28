using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float radius = 3f;
    [SerializeField] Vector2 explosionForce = new Vector2(200f, 100f);
    [SerializeField] AudioClip bombSFX, burnSFX;

    Animator bombAnimator;
    BoxCollider2D bombCollider;
    AudioSource myAudioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        bombCollider = GetComponent<BoxCollider2D>();
        bombAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ExplodeBomb()
    {
       Collider2D playerCollider =  Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Player"));
        if (playerCollider)
        {
            playerCollider.GetComponent<Rigidbody2D>().AddForce(explosionForce);
            playerCollider.GetComponent<Player>().PlayerHit();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bombAnimator.SetTrigger("Explosion");
        myAudioSource.PlayOneShot(burnSFX);
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }
    void bombSound()
    {
        myAudioSource.PlayOneShot(bombSFX);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
