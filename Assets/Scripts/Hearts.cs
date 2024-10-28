using UnityEngine;

public class Hearts : MonoBehaviour
{
    BoxCollider2D heartCollider;
    Rigidbody2D heartRigid;

    // Start is called before the first frame update
    void Start()
    {
        heartCollider = GetComponent<BoxCollider2D>();
        heartRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        pickupHeart();
    }

    private void pickupHeart()
    {
        if (heartCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Destroy(gameObject);
        }
    }
}
