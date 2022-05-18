using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
	public void FindPath(Tile start, Tile target)
	{
		List<Tile> openSet = new List<Tile>();
		HashSet<Tile> closedSet = new HashSet<Tile>();
		openSet.Add(start);

		while (openSet.Count > 0)
		{
			Tile currentTile = openSet[0];

			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].FCost < currentTile.FCost || openSet[i].FCost == currentTile.FCost)
				{
					if (openSet[i].HCost < currentTile.HCost)
						currentTile = openSet[i];
				}
			}

			openSet.Remove(currentTile);
			closedSet.Add(currentTile);

			if (currentTile == target)
			{
				RetracePath(start, target);
				return;
			}

			foreach (Tile neighbour in currentTile.Neighbours)
			{
				if (closedSet.Contains(neighbour))
				{
					continue;
				}

				int newCostToNeighbour = currentTile.GCost + GetDistance(currentTile, neighbour);
				if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
				{
					neighbour.GCost = newCostToNeighbour;
					neighbour.HCost = GetDistance(neighbour, target);
					neighbour.Parent = currentTile;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(Tile startNode, Tile endNode)
	{
		List<Tile> path = new List<Tile>();
		Tile currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode.ColorVisited();
			currentNode = currentNode.Parent;
		}
		path.Reverse();

		path[0].ColorStart();
		path[path.Count - 1].ColorEnd();
	}
	//get mathatan dis
	int GetDistance(Tile tileA, Tile tileB)
	{
		int dstX = Mathf.Abs(tileA.PosX - tileB.PosX);
		int dstY = Mathf.Abs(tileA.PosY - tileB.PosY);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		return 14 * dstX + 10 * (dstY - dstX);
	}
}
