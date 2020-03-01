using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //  Note: all variables below are prefixed "m_"
    //  To mark these variables as those native to this script.
    //  Useful for autocomplete feature during coding.

    //  For tuning player movement values
    public float m_moveSpeed;             //  how fast the player moves on surface
    public float m_jumpSpeed;
    public float m_jumpForce;             //  how high the player jumps

    //  For checking if player is on the ground
    public GameObject m_groundCheckLeft, m_groundCheckRight;
    public float m_groundCheckRadius = 0.5f;
    public LayerMask m_groundLayer;

    //  For customizing controls (currently unused)
    public KeyCode m_moveUp = KeyCode.W;
    public KeyCode m_moveDown = KeyCode.S;
    public KeyCode m_moveLeft = KeyCode.A;
    public KeyCode m_moveRight = KeyCode.D;

    //  Private values are used to do internal calculations.
    private Rigidbody2D m_rb2d;
    private float m_horizontalInput;

    // For setting player animation
    private Animator m_animator;

    //  For playing sounds
    public AudioSource m_SfxSource;
    public AudioClip m_WalkingClip;
    public AudioClip m_JumpClip;


    // Start is called before the first frame update
    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void PlayTrack(AudioClip audioClip)
    {
        m_SfxSource.clip = audioClip;
        if (!m_SfxSource.isPlaying)
            m_SfxSource.Play();
    }

    private bool isGrounded()
    {
        //  Draw a small circle at the player's feet.
        //  If this circle overlaps a platform/the ground, player is grounded, return true.
        bool checkLeft = Physics2D.OverlapCircle(m_groundCheckLeft.transform.position, m_groundCheckRadius, m_groundLayer);
        bool checkRight = Physics2D.OverlapCircle(m_groundCheckRight.transform.position, m_groundCheckRadius, m_groundLayer); 

        return checkLeft || checkRight;
    }

    private void Move()
    {
        Vector2 m_direction = new Vector2(0, 0);   //  which direction is the player going?

        m_horizontalInput = Input.GetAxis("Horizontal");

        m_rb2d.velocity = new Vector2(m_horizontalInput * m_moveSpeed,
                                    m_rb2d.velocity.y);

        if (Mathf.Abs(m_horizontalInput) > 0)
        {
            //  Move
            if (m_horizontalInput > 0f)
            {
                m_animator.SetBool("isRunning", true);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (m_horizontalInput < 0f)
            {
                m_animator.SetBool("isRunning", true);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            //  Play sound
            if (isGrounded())
            {
                PlayTrack(m_WalkingClip);
            }
        }
        else
        {
            //  Set bool
            m_animator.SetBool("isRunning", false);

            //  Stop playing sound
            m_SfxSource.Stop();
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(m_moveUp))
        {
            if (isGrounded())
                //m_rb2d.AddForce(Vector2.up * m_jumpForce);
                m_rb2d.velocity = new Vector2(m_rb2d.velocity.x,
                                            m_jumpSpeed);
                m_animator.SetBool("isJumping", true);
                PlayTrack(m_JumpClip);
        } else
        {
            m_animator.SetBool("isJumping", false);
        }

    }
}
