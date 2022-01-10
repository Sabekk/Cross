using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossButton : MonoBehaviour
{
    public CrossButton(Grid grid, int x, int y)
    {
        this.grid = grid;
        posX = x;
        posY = y;
        isAvalilable = true;
    }

    public int gCost;
    public int hCost;
    public int fCost;
    public Grid grid;
    public int posX;
    public int posY;
    public bool isAvalilable;
    public Text buttonText;

    public CrossButton cameFrom;
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
