using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pathfinding : MonoBehaviour {


    private const int straight_cost = 10;
    private const int diagonal_cost = 14;

    public Grid grid;
    public Cross cross;
    public List<NodeButton> openList;
    public List<NodeButton> closedList;
    public List<NodeButton> FindPath(NodeButton startPosition, NodeButton endPosition)
    {
        openList = new List<NodeButton> { startPosition };
        closedList = new List<NodeButton>();

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
            NodeButton curretPosition = GetLowestFCostPosition(openList);

            if (curretPosition == endPosition)
            {
                return CalculatedPath(endPosition);
            }

            openList.Remove(curretPosition);
            closedList.Add(curretPosition);

            foreach (NodeButton neighbour in GetNeighbourList(curretPosition))
            {
                if (closedList.Contains(neighbour))
                {
                    continue;
                }
                if (!neighbour.isAvalilable)
                {
                    closedList.Add(neighbour);
                    continue;
                }

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

    public List<NodeButton> GetNeighbourList(NodeButton curretPosition)
    {
        List<NodeButton> neighbourList = new List<NodeButton>();

        if (curretPosition.posX - 1 >= 1)
        {
            neighbourList.Add(grid.GetButton(curretPosition.posX - 1, curretPosition.posY));

            if (curretPosition.posY - 1 >= 1)
            {
                neighbourList.Add(grid.GetButton(curretPosition.posX - 1, curretPosition.posY - 1));
            }
            if (curretPosition.posY + 1 <= cross.heightCross)
            {
                neighbourList.Add(grid.GetButton(curretPosition.posX - 1, curretPosition.posY + 1));
            }
        }
        if (curretPosition.posX + 1 <= cross.widthCross)
        {
            neighbourList.Add(grid.GetButton(curretPosition.posX + 1, curretPosition.posY));
            if (curretPosition.posY - 1 >= 1)
            {
                neighbourList.Add(grid.GetButton(curretPosition.posX + 1, curretPosition.posY - 1));
            }
            if (curretPosition.posY + 1 <= cross.heightCross)
            {
                neighbourList.Add(grid.GetButton(curretPosition.posX + 1, curretPosition.posY + 1));
            }
        }
        if (curretPosition.posY - 1 >= 1)
        {
            neighbourList.Add(grid.GetButton(curretPosition.posX, curretPosition.posY - 1));
        }
        if (curretPosition.posY + 1 <= cross.heightCross)
        {
            neighbourList.Add(grid.GetButton(curretPosition.posX, curretPosition.posY + 1));
        }

        return neighbourList;
    }

    public List<NodeButton> CalculatedPath(NodeButton endPosition)
    {
        List<NodeButton> path = new List<NodeButton>();
        //path.Add(endPosition);
        NodeButton curretPosition = endPosition;
        while (curretPosition.cameFrom != null)
        {
            path.Add(curretPosition.cameFrom);
            curretPosition = curretPosition.cameFrom;
        }
        path.Reverse();
        return path;
    }

    public int CalculateDistance(NodeButton a, NodeButton b)
    {
        int distanceX = Mathf.Abs(a.posX - b.posX);
        int distanceY = Mathf.Abs(a.posY - b.posY);
        int remaining = Mathf.Abs(distanceX - distanceY);

        return diagonal_cost * Mathf.Min(distanceX, distanceY) + straight_cost * remaining;
    }

    public NodeButton GetLowestFCostPosition(List<NodeButton> positions)
    {
        NodeButton lowestFCostPosition = positions[0];
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
