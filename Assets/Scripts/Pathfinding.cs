using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pathfinding : MonoBehaviour {


    private const int straight_cost = 10;
    private const int diagonal_cost = 14;

    public Grid grid;
    public Cross cross;
    public List<CrossButton> openList;
    public List<CrossButton> closedList;
    public List<CrossButton> neighbourList = new List<CrossButton>();
    public List<CrossButton> FindPath(CrossButton startPosition, CrossButton endPosition)
    {
        openList = new List<CrossButton> { startPosition };
        closedList = new List<CrossButton>();

        foreach (var item in grid.buttonsDictionary.Values)
        {
            if (item == startPosition) continue;
            item.gCost = int.MaxValue;
            item.CalculateFCost();
            item.cameFrom = null;
        }

        startPosition.gCost = 0;
        startPosition.hCost = CalculateDistance(startPosition, endPosition);
        startPosition.CalculateFCost();

        while (openList.Count > 0)
        {
            CrossButton curretPosition = GetLowestFCostPosition(openList);
            Debug.Log("curretPosition pos: " + curretPosition.posX + " , " + curretPosition.posY);

            if (curretPosition == endPosition)
            {
                return CalculatedPath(endPosition);
            }

            openList.Remove(curretPosition);
            closedList.Add(curretPosition);

            foreach (CrossButton neighbour in GetNeighbourList(curretPosition))
            {
                if (closedList.Contains(neighbour))
                {
                    continue;
                }
                //if (!neighbour.isAvalilable)
                //{
                //    closedList.Add(neighbour);
                //    continue;
                //}

                Debug.Log("neighbour pos: " + neighbour.posX + " , " + neighbour.posY);

                int tentativeGCost = curretPosition.gCost + CalculateDistance(curretPosition, neighbour);

                if (tentativeGCost < neighbour.gCost)
                {
                    neighbour.cameFrom = curretPosition;
                    neighbour.gCost = tentativeGCost;
                    neighbour.hCost = CalculateDistance(neighbour, endPosition);
                    neighbour.CalculateFCost();
                }

                if (!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
                }
            }
        }
        return null;
    }

    public List<CrossButton> GetNeighbourList(CrossButton curretPosition)
    {
        neighbourList.Clear();
        Debug.Log("Curret position to neigbers: " + curretPosition.posX + ", " + curretPosition.posY);

        if (curretPosition.posX - 1 >= 0)
        {
            neighbourList.Add(grid.GetButton(curretPosition.posX - 1, curretPosition.posY));

            if (curretPosition.posY - 1 >= 0)
            {
                neighbourList.Add(grid.GetButton(curretPosition.posX - 1, curretPosition.posY - 1));
            }
            if (curretPosition.posY + 1 < cross.heightCross)
            {
                neighbourList.Add(grid.GetButton(curretPosition.posX - 1, curretPosition.posY + 1));
            }
        }
        if (curretPosition.posX + 1 < cross.widthCross)
        {
            neighbourList.Add(grid.GetButton(curretPosition.posX + 1, curretPosition.posY));
            if (curretPosition.posY - 1 >= 0)
            {
                neighbourList.Add(grid.GetButton(curretPosition.posX + 1, curretPosition.posY - 1));
            }
            if (curretPosition.posY + 1 < cross.heightCross)
            {
                neighbourList.Add(grid.GetButton(curretPosition.posX + 1, curretPosition.posY + 1));
            }
        }
        if (curretPosition.posY - 1 >= 0)
        {
            neighbourList.Add(grid.GetButton(curretPosition.posX, curretPosition.posY - 1));
        }
        if (curretPosition.posY + 1 < cross.heightCross)
        {
            neighbourList.Add(grid.GetButton(curretPosition.posX, curretPosition.posY + 1));
        }

        return neighbourList;
    }

    public List<CrossButton> CalculatedPath(CrossButton endPosition)
    {
        List<CrossButton> path = new List<CrossButton>();
        path.Add(endPosition);
        CrossButton curretPosition = endPosition;
        while (curretPosition.cameFrom != null)
        {
            path.Add(curretPosition.cameFrom);
            curretPosition = curretPosition.cameFrom;
        }
        path.Reverse();
        return path;
    }

    public int CalculateDistance(CrossButton a, CrossButton b)
    {
        int distanceX = Mathf.Abs(a.posX - b.posX);
        int distanceY = Mathf.Abs(a.posY - b.posY);
        int remaining = Mathf.Abs(distanceX - distanceY);

        return diagonal_cost * Mathf.Min(distanceX, distanceY) + straight_cost * remaining;
    }

    public CrossButton GetLowestFCostPosition(List<CrossButton> positions)
    {
        CrossButton lowestFCostPosition = positions[0];
        for (int i = 1; i < positions.Count; i++)
        {
            if (positions[i].fCost < lowestFCostPosition.fCost)
            {
                lowestFCostPosition = positions[i];
            }
        }

        return lowestFCostPosition;
    }
}
