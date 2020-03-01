using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/** Contains methods for manipulating player object's properties.
 *  e.g position and collision
 * */
public class PlayerManager : MonoBehaviour
{
    //  To be assigned
    public TorchListManager m_torchList;
    public BlackOut m_BlackOut;

    //  Fields that can be assigned through code
    private WOF m_WOF;

    //  Control game loop
    public int m_playerLives = 5;
    public bool m_dead = false;
    public bool m_won = false;
    public int m_furthestCheckpointReached = -1;    //  Stores the index of furthest checkpoint 
                                                    //  reached in the hierachy.

    //  Position variables
    [SerializeField] private Vector3 m_playerRespawnPosition = new Vector3(0,0,0);
    public float m_playerRespawnY;      //  To spawn the WOF [units] below the player

    //  UI display
    public Text m_livesText;

    public AudioSource sfxSource;
    public AudioClip deadClip;

    // Start is called before the first frame update
    void Awake()
    {
        //  set up game loop fields
        m_dead = false;
        m_won = false;
        m_furthestCheckpointReached = -1;

        //  Set up UI
        m_livesText.text = ("Lives " + m_playerLives.ToString()); 
    }

    public void Start()
    {
        //  Set up position
        m_playerRespawnPosition = gameObject.transform.position;
        m_playerRespawnY = transform.position.y;

        //  Get WOF script from collider
        m_WOF = FindObjectOfType<WOF>();
    }

    /** Called upon collision with a trigger.
     * */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Checkpoint")
        {
            m_playerRespawnY = transform.position.y;
            m_WOF.ResetPositionToNearCheckpoint();  
        }

        if (other.tag == "SpeedCap")
        {
            m_WOF.SpeedCapIncreased();
        }

        else if (other.tag == "WOF")
        {
            //  Upadte lives and lives display
            m_playerLives--;
            m_livesText.text = ("Lives " + m_playerLives.ToString());

            sfxSource.clip = deadClip;
            sfxSource.Play();

            if (m_playerLives > 0)
            {
                //  Reset positions
                transform.position = m_playerRespawnPosition;
                m_WOF.ResetPositionUponPlayerDeath();

                //  Reset light
                m_BlackOut.ResetWithGraceTime();

                //  Reset torches
                m_torchList.ResetAllTorches();
            }
            else
            {
                m_dead = true;
                Debug.Log("Player Dead");
            }
        }

        else if (other.tag == "LevelExit")
        {
            m_won = true;
        }
    }

    /** Reset the position of the player.
     * */
    public void ChangeResetPosition ()
    {
        m_playerRespawnPosition = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = m_playerRespawnPosition;
        m_WOF.ResetPositionUponPlayerDeath();
    }
}
