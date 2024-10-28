using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] AudioClip diamondPickupSfx;
    [SerializeField] int diamondValue = 100;

    Rigidbody diamondRigid;
    CapsuleCollider2D diamondBody;
    // Start is called before the first frame update
    void Start()
    {
        diamondBody = GetComponent<CapsuleCollider2D>();
        diamondRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PickupDiamond();
    }

    private void PickupDiamond()
    {
        if (diamondBody.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            AudioSource.PlayClipAtPoint(diamondPickupSfx, Camera.main.transform.position);
            FindObjectOfType<GameSession>().addToScore(diamondValue);
            Destroy(gameObject);
        }
    }

     
}
