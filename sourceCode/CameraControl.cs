using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //  m_PlayerTransform will not appear in Unity's Inspector so Unity will not scream "Object not set"
    public Transform playerTransform;
    public bool followPlayer;

    public float topLimit = int.MaxValue;
    public float bottomLimit = -3f;
    public float leftLimit = -22.5f;
    public float rightLimit = 21.6f;
    
    public void Awake()
    {
        followPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            if (followPlayer)
            {
                Vector3 m_newPosition = new Vector3(
                    Mathf.Clamp(playerTransform.position.x, leftLimit, rightLimit),
                    Mathf.Clamp(playerTransform.position.y, bottomLimit, topLimit),
                    -10);
                transform.position = Vector3.Lerp(transform.position, m_newPosition, 0.1f);
            }
        }
        else
        {
            playerTransform = GameObject.Find("Player").transform;
        }
    }
}
