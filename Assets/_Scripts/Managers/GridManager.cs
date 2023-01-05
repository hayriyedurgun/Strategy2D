using Assets._Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class GridManager : MonoBehaviour
    {
        private int m_StraightCost = 10;
        private int m_DiagonalCost = 14;

        private TileBehaviour[,] m_Tiles;

        public int Width = 8;
        public int Height = 5;
        public float CellSize = 1f;

        public TileBehaviour TilePrefab;

        public Vector3 Offset { get; } = new Vector3(-3.35f, -3.5f, 0);

        private static GridManager m_Instance = null;
        public static GridManager Instance => m_Instance;

        private void Awake()
        {
            m_Instance = this;
        }

        private void Start()
        {
            m_Tiles = new TileBehaviour[Width, Height];

            TileBehaviour tile;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    tile = Instantiate(TilePrefab, transform);
                    tile.transform.localPosition = new Vector3(x * CellSize, y * CellSize, 0) + Offset;

                    tile.Init(x, y, CellSize);
                    m_Tiles[x, y] = tile;
                }
            }
        }

        private void OnDestroy()
        {
            m_Instance = null;
        }

        public Vector3 ConvertToWorld(int x, int y)
        {
            return new Vector3(x * CellSize, y * CellSize, 0) + Offset;
        }

        public Vector3 ConvertToGrid(Vector3 wordPos)
        {
            var pos = wordPos - Offset;
            var x = Mathf.RoundToInt(pos.x / CellSize);
            var y = Mathf.RoundToInt(pos.y / CellSize);

            return new Vector3(x, y, 0);
        }

        public TileBehaviour ConvertToTile(Vector3 wordPos)
        {
            var pos = wordPos - Offset;

            var x = Mathf.RoundToInt(pos.x / CellSize);
            var y = Mathf.RoundToInt(pos.y / CellSize);

            if (m_Tiles.GetLength(0) < x + 1 ||
                m_Tiles.GetLength(1) < y + 1 ||
                x < 0 ||
                y < 0)
            {
                return null;
            }

            return m_Tiles[x, y];
        }

        public List<TileBehaviour> GetNeighbors(TileBehaviour tile)
        {
            // Create a list to store the neighbors
            var neighbors = new List<TileBehaviour>();

            // Get the tile's coordinates in the grid
            var x = tile.PosX;
            var y = tile.PosY;

            // Check the tiles to the left, right, top, and bottom of the current tile
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // Skip the current tile
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    // Check if the tile is within the bounds of the grid
                    if (x + i >= 0 && x + i < Width && y + j >= 0 && y + j < Height)
                    {
                        // Add the tile to the list of neighbors
                        neighbors.Add(m_Tiles[x + i, y + j]);
                    }
                }
            }

            // Return the list of neighbors
            return neighbors;
        }

        public TileBehaviour GetNextAvailableNeigbour(TileBehaviour tile)
        {
            var x = tile.PosX;
            var y = tile.PosY;

            for (int horizontalIndex = -1; horizontalIndex <= 1; horizontalIndex++)
            {
                for (int verticalIndex = -1; verticalIndex <= 1; verticalIndex++)
                {
                    // Skip the current tile
                    if (horizontalIndex == 0 && verticalIndex == 0)
                    {
                        continue;
                    }

                    // Check if the tile is within the bounds of the grid
                    if (x + horizontalIndex >= 0 &&
                        x + horizontalIndex < Width &&
                        y + verticalIndex >= 0 &&
                        y + verticalIndex < Height)
                    {
                        var nextTile = m_Tiles[x + horizontalIndex, y + verticalIndex];
                        if (nextTile.Product == null)
                        {
                            return nextTile;
                        }
                    }
                }
            }

            return null;
        }

        public void ClearTiles()
        {
            foreach (var tile in m_Tiles)
            {
                tile.Clear();
            }
        }

        //private List<TileBehaviour> RetracePath(TileBehaviour startTile, TileBehaviour endTile)
        //{
        //    // Create a list to store the path
        //    var path = new List<TileBehaviour>();

        //    // Set the current node to the end tile
        //    var currentNode = endTile;

        //    // Loop until the current node is the start tile
        //    while (currentNode != startTile)
        //    {
        //        // Add the current node to the path
        //        path.Add(currentNode);

        //        // Set the current node to its parent
        //        currentNode = currentNode.Parent;
        //    }

        //    // Reverse the list to order the path from start to end
        //    path.Reverse();

        //    // Return the path
        //    return path;
        //}

        //public List<TileBehaviour> FindPath(Vector3 startPos, Vector3 endPos)
        //{
        //    // Convert the start and end positions to grid coordinates
        //    var startTile = ConvertToTile(startPos);
        //    var endTile = ConvertToTile(endPos);

        //    // Create lists for open and closed nodes
        //    var openNodes = new List<TileBehaviour>();
        //    var closedNodes = new List<TileBehaviour>();

        //    // Add the start tile to the open list
        //    openNodes.Add(startTile);

        //    // Loop until the open list is empty
        //    while (openNodes.Count > 0)
        //    {
        //        // Find the node with the lowest F cost
        //        var currentNode = openNodes[0];
        //        for (int i = 1; i < openNodes.Count; i++)
        //        {
        //            if (openNodes[i].FCost < currentNode.FCost ||
        //                (openNodes[i].FCost == currentNode.FCost && openNodes[i].HCost < currentNode.HCost))
        //            {
        //                currentNode = openNodes[i];
        //            }
        //        }

        //        // Remove the current node from the open list and add it to the closed list
        //        openNodes.Remove(currentNode);
        //        closedNodes.Add(currentNode);

        //        // If the current node is the end node, return the path
        //        if (currentNode == endTile)
        //        {
        //            return RetracePath(startTile, endTile);
        //        }

        //        // Get a list of the current node's neighbors
        //        var neighbors = GetNeighbors(currentNode);
        //        foreach (var neighbor in neighbors)
        //        {
        //            // Skip neighbors that are already in the closed list
        //            if (closedNodes.Contains(neighbor))
        //            {
        //                continue;
        //            }

        //            // Calculate the cost to move to the neighbor
        //            var moveCost = currentNode.GCost + currentNode.GetDistance(neighbor);

        //            // If the neighbor is not in the open list, or the new cost is lower than the current cost, update the neighbor's values and add it to the open list
        //            if (!openNodes.Contains(neighbor) || moveCost < neighbor.GCost)
        //            {
        //                neighbor.GCost = moveCost;
        //                neighbor.HCost = neighbor.GetDistance(endTile);
        //                //neighbor.Parent = currentNode;

        //                if (!openNodes.Contains(neighbor))
        //                {
        //                    openNodes.Add(neighbor);
        //                }
        //            }
        //        }
        //    }

        //    // If no path was found, return an empty list
        //    return new List<TileBehaviour>();
        //}

        public List<TileBehaviour> FindPath(TileBehaviour startTile, TileBehaviour endTile)
        {
            // Create lists for open and closed nodes
            var openTiles = new List<TileBehaviour>();
            var closedTiles = new List<TileBehaviour>();

            // Add the start tile to the open list
            openTiles.Add(startTile);

            //reset
            foreach (var tile in m_Tiles)
            {
                tile.PreviousTile = null;
                tile.GCost = int.MaxValue;
            }

            startTile.GCost = 0;
            startTile.HCost = CalculateDistance(startTile, endTile);

            while (openTiles.Count > 0)
            {
                var currentTile = openTiles.OrderBy(x => x.FCost).FirstOrDefault();
                if (currentTile == endTile)
                {
                    return CalculatePath(endTile);
                }

                openTiles.Remove(currentTile);
                closedTiles.Add(currentTile);

                foreach (var neighborTile in GetNeighbors(currentTile))
                {
                    if (!neighborTile.IsAvailable)
                    {
                        closedTiles.Add(neighborTile);
                    }

                    if (closedTiles.Contains(neighborTile))
                    {
                        continue;
                    }

                    var tempGCost = currentTile.GCost + CalculateDistance(currentTile, neighborTile);
                    if (tempGCost < neighborTile.GCost)
                    {
                        neighborTile.PreviousTile = currentTile;
                        neighborTile.GCost = tempGCost;
                        neighborTile.HCost = CalculateDistance(neighborTile, endTile);

                        if (!openTiles.Contains(neighborTile))
                        {
                            openTiles.Add(neighborTile);
                        }
                    }
                }

            }

            return null;
        }

        private int CalculateDistance(TileBehaviour from, TileBehaviour to)
        {
            var xDistance = Mathf.Abs(from.PosX - to.PosX);
            var yDistance = Mathf.Abs(from.PosY - to.PosY);
            var remaining = Mathf.Abs(xDistance - yDistance);

            return Mathf.Min(xDistance, yDistance) * m_DiagonalCost + remaining * m_StraightCost;
        }

        private List<TileBehaviour> CalculatePath(TileBehaviour endTile)
        {
            var tiles = new List<TileBehaviour>();
            tiles.Add(endTile);

            var currentTile = endTile;

            while (currentTile.PreviousTile != null)
            {
                tiles.Add(currentTile.PreviousTile);
                currentTile = currentTile.PreviousTile;
            }

            tiles.Reverse();
            return tiles;
        }
    }
}
