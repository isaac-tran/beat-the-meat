using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    private KeyCode startKey = KeyCode.Space;
    private KeyCode quitKey = KeyCode.Escape;
    public string gameScene;

    public MenuManager menuOptions;
    public OverlayScreenUI transitionOverlayScreen;
    public Text loadingText;

    public AudioSource musicSource;
    public AudioSource sfxSource;
      
    private bool transitionPlaying = false;    //  If transition is playing, stop current process
    private bool executingOption = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFadeTransition(Color.black, 1f, 1f, 0f));

        loadingText.gameObject.SetActive(false);
        musicSource.Play();
    }

    void Update()
    {
        //OneButtonAwayMenu();
        ExecuteChoice();
    }

    private void OneButtonAwayMenu()
    {
        if (Input.GetKeyDown(startKey))
        {
            StartCoroutine(LoadGameScene());
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(QuitGame());
        }
    }

    private void ExecuteChoice()
    {
        if (menuOptions.finalChoice == 0 && executingOption == false)
        {
            executingOption = true;
            StartCoroutine(LoadGameScene());
        }


        if (menuOptions.finalChoice == 1 && executingOption == false)
        {
            executingOption = true;
            StartCoroutine(QuitGame());
        }
    }

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

    public IEnumerator LoadGameScene()
    {
        loadingText.gameObject.SetActive(true);

        //  Fade the screen to black
        yield return (StartFadeTransition(Color.black, 0.5f, 0f, 1f));

        //  Load the game scene
        SceneManager.LoadSceneAsync(gameScene);
        Debug.Log("Scene loaded");

        yield return null;
    }

    public IEnumerator QuitGame()
    {
        //  Fade the screen to white
        yield return (StartFadeTransition(Color.white, 1f, 0f, 1f));

        //  Exit application
        Application.Quit();

        yield return null;
    }
}
