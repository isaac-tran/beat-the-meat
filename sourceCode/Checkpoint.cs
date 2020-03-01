using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Sprite redCheckpoint;
    public Sprite greenCheckpoint;

    private PlayerManager playerManager;
    private SpriteRenderer spriteRenderer;
    public bool checkpointActivated;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = redCheckpoint;
        checkpointActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "myPlayer")
        {
            if (!checkpointActivated)
            {
                playerManager = other.GetComponent<PlayerManager>();

                //  Check if the checkpoint is the furthest one
                if (transform.GetSiblingIndex() > playerManager.m_furthestCheckpointReached)
                {
                    //  Change spawn position, and mark this one as the furthest checkpoint reached.
                    playerManager.ChangeResetPosition();
                    playerManager.m_furthestCheckpointReached = transform.GetSiblingIndex();

                    spriteRenderer.sprite = greenCheckpoint;
                    checkpointActivated = true;
                }
            }         
        }
    }
}







