using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour {

    public Pathfinding pathfinding;

    public NodeButton crossButton;

    public NodeButton startButton;
    public NodeButton endButton;

    public Dictionary<Tuple<int, int>, NodeButton> buttonsDictionary = new Dictionary<Tuple<int, int>, NodeButton>();
    public List<NodeButton> path;

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

    public void SetButton(NodeButton button)
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
            else
            {
                endButton.SetColor(unselectedColor);
                endButton.HideButtonPosition();
                endButton = null;
                return;
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
                if (endButton == null)
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
        int disableButtonsCount = (int)((widthCount * heightCount) * 0.1f);

        for (int i = 1; i <= heightCount; i++)
        {
            for (int j = 1; j <= widthCount; j++)
            {
                NodeButton spawnedButton = Instantiate(crossButton, transform);
                spawnedButton.SetButtonValues(this, j, i);
                var key = new Tuple<int, int>(spawnedButton.posX, spawnedButton.posY);
                buttonsDictionary.Add(key, spawnedButton);
            }
        }


        System.Random rand = new System.Random();

        while (disableButtonsCount > 0)
        {
            NodeButton randomButton = GetButton(rand.Next(widthCount), rand.Next(heightCount));
            if (randomButton != null)
            {
                randomButton.SetDisabled();
                disableButtonsCount--;
            }


            if (disableButtonsCount > 0 && randomButton != null)
            {
                List<NodeButton> neighbours = pathfinding.GetNeighbourList(randomButton);
                NodeButton randomNeighbour = neighbours[rand.Next(0, neighbours.Count)];
                if (randomNeighbour != null)
                {
                    randomNeighbour.SetDisabled();
                    disableButtonsCount--;

                }
            }
        }
    }

    public void ShowPath()
    {
        path = pathfinding.FindPath(startButton, endButton);

        if (path != null)
        {
            for (int i = 1; i < path.Count; i++)
            {
                path[i].SetColor(pathColor);
                path[i].ShowButtonPosition(i);
            }
        }
        else
        {
            Debug.Log("Path not found");
        }
    }

    public void ClearPath()
    {
        if (path != null)
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
    }

    public void ClearHolder()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        buttonsDictionary.Clear();
    }

    public NodeButton GetButton(int x, int y)
    {
        NodeButton button;
        var key = new Tuple<int, int>(x, y);

        if (buttonsDictionary.TryGetValue(key, out button))
        {
            return button;
        }
        else
        {
            return null;
        }
    }
}
