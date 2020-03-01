using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchListManager : MonoBehaviour
{
    public void ResetAllTorches()
    {
        int numberOfTorches = transform.childCount;

        for (int i = 0; i < numberOfTorches; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
