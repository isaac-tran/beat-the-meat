using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** A class for managing title/gameover screens as well as menu screen,
 *  any type of UI that pauses the game and covers the whole screen
 * */

public class OverlayScreenUI : MonoBehaviour
{
    public Image m_backgroundImage;

    void Awake()
    {
        SetAlpha(0);
    }

    public void SetAlpha( float newAlpha )
    {
        m_backgroundImage.color = new Color(m_backgroundImage.color.r,
                                            m_backgroundImage.color.g,
                                            m_backgroundImage.color.b,
                                            newAlpha);
    }

    /** Interpolates the UI's alpha value between [0..1]
     *  Set duration to 0 or less than 0 to make transition an instant.
     * */
    public IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        Debug.Log("Playing Fade");

        if (duration <= 0)
            duration = Time.deltaTime;

        startAlpha = Mathf.Clamp(startAlpha, 0, 1);
        endAlpha = Mathf.Clamp(endAlpha, 0, 1);
        SetAlpha(startAlpha);

        float changePerFrame = (Mathf.Abs(startAlpha - endAlpha) / duration) * Time.deltaTime;

        //  Fadeout
        if ( startAlpha > endAlpha )
        {
            while (m_backgroundImage.color.a > endAlpha)
            {
                //  Update the background's alpha
                SetAlpha(Mathf.Clamp(m_backgroundImage.color.a - changePerFrame, 0, 1));
                
                //  Pause the coroutine - come back next frame
                //  This is to prevent the loop finishing the transition IN 1 FRAME.
                yield return null;
            }
        }

        //  Fadeout
        else if (startAlpha < endAlpha)
        {
            while (m_backgroundImage.color.a < endAlpha)
            {
                //  Update the background's alpha for this frame
                SetAlpha(Mathf.Clamp(m_backgroundImage.color.a + changePerFrame, 0, 1));

                //  Pause the coroutine - come back next frame
                //  This is to prevent the loop finishing the transition IN 1 FRAME.
                yield return null;
            }
        }

        yield return null;
    }

    /** Set the background of the screen
     * */
    public void SetBackgroundImage( Sprite sprite )
    {
        m_backgroundImage.sprite = sprite;
    }

    public Color GetBackgroundColorComponent()
    {
        return m_backgroundImage.color;
    }
}
