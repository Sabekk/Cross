using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour {

    public CrossButton startButton;
    public CrossButton endButton;

    public Dictionary<int[], CrossButton> buttonsDictionary = new Dictionary<int[], CrossButton>();

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
        Vector2 newCellSize = new Vector2(width / x, height / y);
        buttonsGrid.cellSize = newCellSize;
    }

    public void SpawnButtons(int widthCount, int heightCount)
    {
        for (int i = 0; i < heightCount; i++)
        {
            for (int j = 0; j < widthCount; j++)
            {
                CrossButton spawnedButton = Instantiate(new CrossButton(this, i, j), transform);
                buttonsDictionary.Add(spawnedButton.GetPosition(), spawnedButton);
            }
        }
    }

    public void ClearHolder()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        buttonsDictionary.Clear();
    }

    public CrossButton GetButton(int x, int y)
    {
        CrossButton button = null;
        int[] position = { x, y };

        if (buttonsDictionary.TryGetValue(position, out button))
        {
            return button;
        }
        else
        {
            Debug.Log("Position not found");
            return null;
        }
    }
}
