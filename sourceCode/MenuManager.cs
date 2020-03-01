using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Image cursor;
    public AudioSource sfxSource;

    public KeyCode keyUp1 = KeyCode.W;
    public KeyCode keyUp2 = KeyCode.UpArrow;
    public KeyCode keyDown1 = KeyCode.S;
    public KeyCode keyDown2 = KeyCode.DownArrow;
    public KeyCode confirmKey1 = KeyCode.Space;

    private Transform[] options;
    private int choiceIndex = 0;
    private int maximumChoices = 0;
    public int finalChoice = -1;

    public void Awake()
    {
        maximumChoices = gameObject.transform.childCount - 1;
        for (int i = 0; i < maximumChoices; i++)
        {
            //options[i] = transform.GetChild(i);
        }

        finalChoice = -1;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyUp1) || Input.GetKeyDown(keyUp2))
        {
            choiceIndex = Mathf.Clamp(choiceIndex - 1, 0, maximumChoices - 1);
            cursor.transform.position = new Vector2(
                cursor.transform.position.x,
                transform.GetChild(choiceIndex).position.y);

            if (sfxSource.isPlaying == false)
                sfxSource.Play();
        }

        if (Input.GetKeyDown(keyDown1) || Input.GetKeyDown(keyDown2))
        {
            choiceIndex = Mathf.Clamp(choiceIndex + 1, 0, maximumChoices - 1);
            cursor.transform.position = new Vector2(
                cursor.transform.position.x,
                transform.GetChild(choiceIndex).position.y);

            if (sfxSource.isPlaying == false)
                sfxSource.Play();
        }

        if (Input.GetKeyDown(confirmKey1) && finalChoice == -1)
        {
            finalChoice = choiceIndex;
        }
    }
}
