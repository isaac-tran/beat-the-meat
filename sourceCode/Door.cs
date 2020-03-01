using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject endDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "myPlayer")
        {
            other.transform.position = endDoor.transform.position;
            Debug.Log("teleported");
        }
    }
}
