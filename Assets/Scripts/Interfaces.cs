using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IButton {

    public Grid grid { get; set; }
    public int posX { get; set; }
    public int posY { get; set; }
    public bool isAvalilable { get; set; }

    public int gCost { get; set; }
    public int hCost { get; set; }
    public int fCost { get; set; }

    public NodeButton cameFrom { get; set; }

    public void CalculateFCost();
    public void ShowButtonPosition();
    public void ShowButtonPosition(int pos);
    public void HideButtonPosition();
    public void SetButtonOnGrid();
    public void SetButtonValues(Grid grid, int x, int y);
    public void SetColor(Color newColor);
    public void SetDisabled();
}
