using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButton {

    public Grid grid { get; set; }
    public int posX { get; set; }
    public int posY { get; set; }
    public bool avalilable { get; set; }

    public int gCost { get; set; }
    public int hCost { get; set; }
    public int fCost { get; set; }
}
