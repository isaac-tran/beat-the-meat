using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CameraControl m_Camera;

    //  Links to other scenes
    public string m_titleSceneName;
    public string m_gameOverSceneName;
    public string m_gameWonSceneName;

    //  For transition effects
    public OverlayScreenUI transitionOverlayScreen;
    public Image m_gamePauseOverlayScreen;
    public MenuManager pauseMenuOptions;

    public GameObject m_Player;
    public GameObject m_WOF;
    public BlackOut m_BlackoutScreen;

    public Transform m_PlayerSpawnPoint;
    public Transform m_WOFSpawnPoint;

    public AudioSource m_MusicSource;
    public AudioClip m_LevelThemeClip;

    private KeyCode m_StartGameKey = KeyCode.Space;
    private KeyCode m_PauseKey = KeyCode.Escape;
    private KeyCode m_QuitKey = KeyCode.Q;
    private KeyCode m_TitleKey = KeyCode.T;

    private bool transitionPlaying = false;


    //  For manipulating game flow
    private bool m_loseCondition;
    private bool m_winCondition;

    //  Awake is called when the script is enabled
    private void Awake()
    {
        m_winCondition = false;
        m_loseCondition = false;

        InitElements();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameLoop());
    }

    //  Fades the screen to a certain solid colour for a duration
    private IEnumerator StartFadeTransition(Color overlayColor, float duration, 
                                        float startAlpha, float endAlpha)
    {
        transitionPlaying = true;

        Debug.Log("starting transition");

        //  Change color of overlay screen
        transitionOverlayScreen.gameObject.GetComponent<Image>().color = overlayColor;

        //  Fades the title screen to overlayColor
        yield return (transitionOverlayScreen.Fade(startAlpha, endAlpha, duration));

        transitionPlaying = false;

        yield return null;
    }

    private IEnumerator GameLoop()
    {
        yield return StartFadeTransition(Color.black, 0.5f, 1f, 0f);

        Debug.Log("Game initiating");
        yield return GameStart();
        Debug.Log("Game initiated");

        Debug.Log("Game playing");
        yield return GamePlay();
        Debug.Log("Game finished playing");

        Debug.Log("m_winCondition = " + m_winCondition);
        Debug.Log("m_loseCondition = " + m_loseCondition);

        if (m_winCondition)
        {
            Debug.Log("Game winning");
            yield return GameWon();
            Debug.Log("Game won");
        }
        else if (m_loseCondition)
        {
            Debug.Log("Game ending");
            yield return GameOver();
            Debug.Log("Game ended");
        }

        Debug.Log("game loop coroutine has stopped. Waiting for reset");

        while (Input.GetKeyDown(m_StartGameKey) == false)
            yield return null;

        yield return GameLoop();
    }
       
    private IEnumerator GameStart()
    {
        InitElements();

        yield return null;
    }

    private IEnumerator GamePlay()
    {
        PlayerManager m_PlayerManager = m_Player.GetComponent<PlayerManager>();

        //  Update conditions before playing
        m_winCondition = m_PlayerManager.m_won;
        m_loseCondition = m_PlayerManager.m_dead;

        //  Keep playing until player falls into the wall
        while (m_winCondition == false && m_loseCondition == false)
        {
            //  Update conditions
            m_winCondition = m_PlayerManager.m_won;
            m_loseCondition = m_PlayerManager.m_dead;

            if (Input.GetKeyDown(m_PauseKey))
                yield return GamePause();

            yield return null;
        }

        yield return null;
    }

    private IEnumerator GamePause()
    {
        bool m_optionChosen = false;

        m_MusicSource.Pause();

        m_gamePauseOverlayScreen.gameObject.SetActive(true);

        Time.timeScale = 0.0f;

        while (!m_optionChosen)
        {
            /*
            if (Input.GetKeyDown(m_StartGameKey))
                m_optionChosen = true;
            
            else if (Input.GetKeyDown(m_QuitKey))
            {
                m_optionChosen = true;
                SceneManager.LoadSceneAsync(m_titleSceneName);
            }
            */

            if (pauseMenuOptions.finalChoice == 0)
                m_optionChosen = true;

            else if (pauseMenuOptions.finalChoice == 1)
            {
                m_optionChosen = true;
                SceneManager.LoadSceneAsync(m_titleSceneName);
            }

            pauseMenuOptions.finalChoice = -1;

            yield return null;
        }

        Time.timeScale = 1f;

        m_MusicSource.Play();

        m_gamePauseOverlayScreen.gameObject.SetActive(false);

        yield return null;
    }

    private IEnumerator GameOver()
    {
        yield return StartFadeTransition(Color.black, 1f, 0f, 1f);

        SceneManager.LoadSceneAsync(m_gameOverSceneName);

        yield return null;
    }

    private IEnumerator GameWon()
    {
        yield return StartFadeTransition(Color.white, 1f, 0f, 1f);

        SceneManager.LoadSceneAsync(m_gameWonSceneName);

        yield return null;
    }

    /** Initialise game elements
     *  */
    private void InitElements()
    {
        m_gamePauseOverlayScreen.gameObject.SetActive(false);

        //  Instantiate a player object into the game, and set up camera to follow the player
        m_Player.transform.position = m_PlayerSpawnPoint.position;
        m_Camera.playerTransform = m_Player.transform;
        m_Camera.followPlayer = true;

        //  Spawn WOF 
        m_WOF.transform.position = m_WOFSpawnPoint.position;
        
        //  Reset blackout to 0 with grace time added
        m_BlackoutScreen.ResetWithGraceTime();

        //  Play music
        m_MusicSource.clip = m_LevelThemeClip;
        m_MusicSource.Play();
    }

    /** Destroy current game's instantiations for a new round of game
     *  */
    private void CleanUp()
    {
        Destroy(m_Player);
        Destroy(m_WOF);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
