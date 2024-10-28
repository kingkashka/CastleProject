using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartPickup : MonoBehaviour
{
    [SerializeField] AudioClip heartSFX;
    //[SerializeField] int heartValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(heartSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().addToLife();
        Destroy(gameObject);
    }

    
}
