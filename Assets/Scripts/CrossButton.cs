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

    public CrossButton cameFrom;
    public Image buttonImage;

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public int[] GetPosition()
    {
        int[] position = { posX, posY };

        return position;
    }

    public void SetButtonOnGrid()
    {
        grid.SetButton(this);   
    }
    
    public void SetButtonValues(Grid grid, int x, int y)
    {
        this.grid = grid;
        posX = x;
        posY = y;
        isAvalilable = true;
    }

    public void SetColor(Color newColor)
    {
        buttonImage.color = newColor;
    }
}
