using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWonScreenManager : MonoBehaviour
{
    private KeyCode startKey = KeyCode.Space;
    private KeyCode titleKey = KeyCode.T;
    private KeyCode quitKey = KeyCode.Escape;
    public string gameSceneName;
    public string titleSceneName;

    public AudioSource musicSource;

    public OverlayScreenUI transitionOverlayScreen;
    public MenuManager menuOptions;
    public Text loadingText;
    private bool transitionPlaying;
    private bool executingOption = false;

    // Start is called before the first frame update
    void Start()
    {
        loadingText.gameObject.SetActive(false);
        //  Fade out from black
        StartCoroutine(StartFadeTransition(Color.black, 1f, 1f, 0f));
    }

    void Update()
    {
        if (menuOptions.finalChoice == 0 && executingOption == false)
        {
            executingOption = true;
            StartCoroutine(LoadGameScene());
        }


        if (menuOptions.finalChoice == 1 && executingOption == false)
        {
            executingOption = true;
            StartCoroutine(ReturnToTitleScreen());
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
        SceneManager.LoadSceneAsync(gameSceneName);
        Debug.Log("Scene loaded");

        yield return null;
    }

    private IEnumerator ReturnToTitleScreen()
    {
        loadingText.gameObject.SetActive(true);

        yield return (StartFadeTransition(Color.black, 0.5f, 0f, 1f));

        SceneManager.LoadSceneAsync(titleSceneName);
        Debug.Log("Scene loaded");

        yield return null;
    }

    private IEnumerator QuitGame()
    {
        StartFadeTransition(Color.white, 0.5f, 0f, 1f);

        Application.Quit();

        yield return null;
    }
}
