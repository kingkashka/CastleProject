using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator bombAnimator;
    BoxCollider2D bombCollider;
    // Start is called before the first frame update
    void Start()
    {
        bombCollider = GetComponent<BoxCollider2D>();
        bombAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bombAnimator.SetTrigger("Explosion");
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }



}
