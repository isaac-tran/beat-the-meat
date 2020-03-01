using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    //Had to give player rigidbody 2d to test this, set players gravity to zero to stop it falling off screen.

    private BlackOut blackOut;

    // Start is called before the first frame update
    void Start()
    {
        blackOut = FindObjectOfType<BlackOut>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) //Deactivates torch game object and resets lightsGetDim in Blackout script to zero - demonstrable by running game and dragging player object into torch object
    {
        if (other.tag == "myPlayer")
        {
            blackOut.ResetWithGraceTime();
            gameObject.SetActive(false);
        }
    }

    
}
