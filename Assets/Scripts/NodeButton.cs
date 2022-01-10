using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeButton : MonoBehaviour, IButton
{

    public int gCost { get; set; }
    public int hCost { get; set; }
    public int fCost { get; set; }
    public Grid grid { get; set; }
    public int posX { get; set; }
    public int posY { get; set; }
    public bool isAvalilable { get; set; }

    public NodeButton cameFrom { get; set; }

    public Text buttonText;
    public Image buttonImage;

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void ShowButtonPosition()
    {
        buttonText.text = posX + " , " + posY;
        buttonText.gameObject.SetActive(true);
    }
    public void ShowButtonPosition(int pos)
    {
        buttonText.text = posX + " , " + posY + " {" + pos +"} ";
        buttonText.gameObject.SetActive(true);
    }

    public void HideButtonPosition()
    {
        buttonText.gameObject.SetActive(false);
    }

    public void SetButtonOnGrid()
    {
        if (isAvalilable)
        {
            grid.SetButton(this);
        }
    }
    
    public void SetButtonValues(Grid grid, int x, int y)
    {
        this.grid = grid;
        posX = x;
        posY = y;
        isAvalilable = true;
        cameFrom = null;
    }

    public void SetColor(Color newColor)
    {
        buttonImage.color = newColor;
    }

    public void SetDisabled()
    {
        isAvalilable = false;
        SetColor(grid.disabledColor);
    }
}
