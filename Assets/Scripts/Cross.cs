using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cross : MonoBehaviour
{
    public CrossButton crossButton;

    public InputField inputFieldX;
    public InputField inputFieldY;

    public float widthCross;
    public float heightCross;

    public ButtonsHolder buttonsHolder;


    public void ChangeCrossWidth()
    {
        widthCross = float.Parse(inputFieldX.text);
    }
    public void ChangeCrossHeight()
    {
        heightCross = float.Parse(inputFieldY.text);
    }

    public void GenerateCross()
    {
        buttonsHolder.SetCellSize(widthCross, heightCross);

        if (buttonsHolder.transform.childCount > 0)
        {
            buttonsHolder.ClearHolder();
        }

        SpawnButtons();
    }

    public void SpawnButtons()
    {
        int buttonsCount = (int)(widthCross * heightCross);
        int buttonsDisabled = (int)(buttonsCount * 0.1f);

        for (int i = 0; i < buttonsCount; i++)
        {
            Instantiate(crossButton, buttonsHolder.transform);
        }
    }
}
