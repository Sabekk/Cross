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
    public Color disabledColor;
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
        if (startButton == null)
        {
            if (button != endButton)
            {
                startButton = button;
                startButton.SetColor(selectedStartColor);
                startButton.ShowButtonPosition();
                if (path.Count == 0 && endButton != null)
                {
                    ShowPath();
                }
            }
        }
        else
        {
            if (startButton == button)
            {
                startButton.SetColor(unselectedColor);
                startButton.HideButtonPosition();
                startButton = null;
                ClearPath();
            }
            else
            {
                if (endButton == null /*&& button != startButton*/)
                {
                    endButton = button;
                    endButton.SetColor(selectedEndColor);
                    endButton.ShowButtonPosition();
                    ShowPath();
                }
                else
                {
                    if (endButton == button)
                    {
                        endButton.SetColor(unselectedColor);
                        endButton.HideButtonPosition();
                        endButton = null;
                        ClearPath();
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
                CrossButton spawnedButton = Instantiate(crossButton, transform);
                spawnedButton.SetButtonValues(this, i, j);
                var key = new Tuple<int, int>(spawnedButton.posX, spawnedButton.posY);
                buttonsDictionary.Add(key, spawnedButton);
            }
        }       
    }

    public void SetDisabledButtons(int disableButtonsCount)
    {
        System.Random rand = new System.Random();

        while (disableButtonsCount > 0)
        {
            CrossButton randomButton = GetButton(rand.Next(0, buttonsDictionary.Count), rand.Next(0, buttonsDictionary.Count));
            randomButton.SetDisabled();
            disableButtonsCount--;

            if (disableButtonsCount > 0)
            {
                List<CrossButton> neighbours = pathfinding.GetNeighbourList(randomButton);
                CrossButton randomNeighbour = neighbours[rand.Next(0, neighbours.Count)];
                randomNeighbour.SetDisabled();
                disableButtonsCount--;
            }

        }
    }

    public void ShowPath()
    {
        path = pathfinding.FindPath(startButton, endButton);

        for (int i = 1; i < path.Count; i++)
        {
            path[i].SetColor(pathColor);
            path[i].ShowButtonPosition(i);
        }
    }
    public void ClearPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            path[i].SetColor(unselectedColor);
            path[i].HideButtonPosition();
        }
        pathfinding.openList.Clear();
        pathfinding.closedList.Clear();
        path.Clear();
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
        CrossButton button;
        var key = new Tuple<int, int>(x, y);

        if (buttonsDictionary.TryGetValue(key, out button))
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
