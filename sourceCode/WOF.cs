using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WOF: MonoBehaviour
{
    public float m_movingUpwardSpeed;    //  How fast wall goes up
    public float m_increaseUpwardSpeedBy;
    public float m_currentSavedUpwardSpeed;
    public float m_speedCap = 1.5f;

    private float throwWOFBackDown;
    private float currentPlayerPosition;

    private Vector3 m_wallRespawnPosition;  //  Internally stores position for future position resets.
	private Rigidbody2D m_myRigidbody;      //  Rigidbody component of the WOF object
    private PlayerManager playerManager;


    // Start is called before the first frame update
    void Start()
    {
        m_myRigidbody = GetComponent<Rigidbody2D>();
		m_wallRespawnPosition = transform.position;
        playerManager = FindObjectOfType<PlayerManager>();
        m_currentSavedUpwardSpeed = m_movingUpwardSpeed;
        throwWOFBackDown = 30f; //roughly half the screen height, maybe can get half screen height from Unity in case of changed aspect ratio??
    }

    // Update is called once per frame
    void Update()
    {
        //  Set velocity of WOF
        m_myRigidbody.velocity = new Vector3(0f, m_movingUpwardSpeed, 0f);

        //  Increase velocity for next update, then capped
        m_movingUpwardSpeed = m_movingUpwardSpeed + m_increaseUpwardSpeedBy * Time.deltaTime;
        m_movingUpwardSpeed = Mathf.Clamp(m_movingUpwardSpeed, int.MinValue, m_speedCap);
    }

    public void SpeedCapIncreased ()
    {
        m_speedCap = 6f;
    }

    public void ResetPositionToNearCheckpoint()
	{
        //m_wallRespawnPosition = transform.position;
        m_wallRespawnPosition = new Vector3(transform.position.x, throwWOFBackDown, transform.position.z);
        m_currentSavedUpwardSpeed = m_movingUpwardSpeed;

    }

    public void ResetPositionUponPlayerDeath()
	{
        //transform.position = m_wallRespawnPosition;
        m_movingUpwardSpeed = m_currentSavedUpwardSpeed;
        currentPlayerPosition = playerManager.m_playerRespawnY - throwWOFBackDown;
        transform.position = new Vector3(transform.position.x, currentPlayerPosition, transform.position.z);

        Debug.Log("player Respawn Y: " + playerManager.m_playerRespawnY);
        Debug.Log("current player position: " + currentPlayerPosition); 
    }
}
