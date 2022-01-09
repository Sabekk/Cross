using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour {

    public Pathfinding pathfinding;

    public CrossButton crossButton;

    public CrossButton startButton;
    public CrossButton endButton;

    public Dictionary<Tuple<int, int>, CrossButton> buttonsDictionary = new Dictionary<Tuple<int, int>, CrossButton>();
    public List<CrossButton> path;

    public GridLayoutGroup buttonsGrid;
    public RectTransform rectTransform;
    public float width;
    public float height;

    public Color unselectedColor;
    public Color selectedStartColor;
    public Color selectedEndColor;
    public Color pathColor;

    private void Start()
    {
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;
    }

    public struct Tuple<T1, T2> {
        public readonly T1 Item1;
        public readonly T2 Item2;

        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }

    public void SetButton(CrossButton button)
    {
        CrossButton tmp;
        if(buttonsDictionary.TryGetValue(button.GetPosition(), out tmp))
        {
            Debug.Log("Jest");
        }
        else
        {
            Debug.Log("Nie ma");
        }

        if (startButton == null)
        {
            startButton = button;
            startButton.SetColor(selectedStartColor);
        }
        else
        {
            if(startButton== button)
            {
                startButton.SetColor(unselectedColor);
                startButton = null;
                path.Clear();
            }
            else
            {
                if (endButton == null)
                {
                    endButton = button;
                    endButton.SetColor(selectedEndColor);
                    path = pathfinding.FindPath(startButton.posX, startButton.posY, endButton.posX, endButton.posY);
                }
                else
                {
                    if (endButton == button)
                    {
                        endButton.SetColor(unselectedColor);
                        endButton = null;
                        path.Clear();
                    }
                }
            }
        }
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
                CrossButton spawnedButton = crossButton;
                spawnedButton.SetButtonValues(this, i, j);
                Instantiate(spawnedButton, transform);
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
