using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackOut : MonoBehaviour
{
    public SpriteRenderer thisBlackOut;

    private float lightsGetDim = -1f; //  represents current level of blackscreens alpha 
                                      //  (0 invisble, 1 totally black) 

    public float m_GraceTime = 5;     //  gives longer period of complete visibility before screen starts dimming.

    public float dimmerSpeed = 0.05f; //  rate at which alpha goes up by per second,
                                      //  bigger number means darkness falls faster, will need fine tuning later.

    // Start is called before the first frame update
    void Start()
    {
        lightsGetDim = -(m_GraceTime * dimmerSpeed); 
        thisBlackOut = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lightsGetDim = lightsGetDim + dimmerSpeed * Time.deltaTime;
        thisBlackOut.color = new Color(1, 1, 1, lightsGetDim);
    }

    public void ResetWithGraceTime()
    {
        lightsGetDim = -(m_GraceTime * dimmerSpeed);
    }

    public void ResetWithoutGraceTime()
    {
        lightsGetDim = 0f;
    }
}
