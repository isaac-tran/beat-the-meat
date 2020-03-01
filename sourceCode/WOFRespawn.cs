using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WOFRespawn : MonoBehaviour
{
    public Transform playerPosition;

    public void ResetWOFPositionWithPlayer()
    {
        Vector3 temp = new Vector3(0, -7f, 0);
        transform.position += temp;
        //transform.position.x = playerPosition - 
        //transform.position = new Vector3(transform.position.x, playerPosition.position.y, -10);
       // transform.position = new Vector3(10, 10, -10);
    }
}
