using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsHolder : MonoBehaviour {

    public GridLayoutGroup buttonsGrid;
    public RectTransform rectTransform;
    public float width;
    public float height;

    private void Start()
    {
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;
    }

    public void SetCellSize(float x, float y)
    {
        Vector2 newCellSize = new Vector2(width / x, height/ y);
        buttonsGrid.cellSize = newCellSize;
    }

    public void ClearHolder()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
