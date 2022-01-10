using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cross : MonoBehaviour
{
    public InputField inputFieldX;
    public InputField inputFieldY;

    public int widthCross;
    public int heightCross;

    public Grid buttonsHolder;


    public void ChangeCrossWidth()
    {
        widthCross = int.Parse(inputFieldX.text);
    }
    public void ChangeCrossHeight()
    {
        heightCross = int.Parse(inputFieldY.text);
    }

    public void GenerateCross()
    {
        buttonsHolder.SetCellSize(widthCross, heightCross);

        if (buttonsHolder.transform.childCount > 0)
        {
            buttonsHolder.ClearHolder();
        }

        buttonsHolder.SpawnButtons(widthCross, heightCross);
    }
}
