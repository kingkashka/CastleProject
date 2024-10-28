using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    Animator exitDoorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetTrigger("DoorExit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
