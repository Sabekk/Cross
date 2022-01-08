using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossButton : MonoBehaviour, IButton
{
    public CrossButton(Grid grid, int x, int y)
    {
        this.grid = grid;
        posX = x;
        posY = y;
    }

    public int gCost { get; set; }
    public int hCost { get; set; }
    public int fCost { get; set; }

    public Grid grid { get; set; }
    public int posX { get; set; }
    public int posY { get; set; }
    public bool avalilable { get; set; }

    public CrossButton cameFrom;

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public int[] GetPosition()
    {
        int[] position = { posX, posY };

        return position;

    }
    
}
