using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private TileGrid _grid;
    private List<Tile> openList;
    private List<Tile> closedList;

    public Pathfinding(TileGrid grid)
    {
        _grid = grid;
    }

    public List<Tile> FindPath(int startX, int startY, int endX, int endY)
    {
        Tile startTile = _grid.GetTile(startX, startY);
        Tile endTile = _grid.GetTile(endX, endY);

        openList = new List<Tile>() { startTile };
        closedList = new List<Tile>();

        for (int x = 0; x < _grid.Width; x++)
            for (int y = 0; y < _grid.Height; y++)
            {
                var tile = _grid.GetTile(x, y);
                tile.gCost = int.MaxValue;
                tile.previus = null;
            }

        startTile.gCost = 0;
        startTile.hCost = DistanceCost(startTile, endTile);

        while(openList.Count > 0)
        {
            Tile currentTile = LowestFCostTile(openList);
            if(currentTile == endTile)
            {
                return CalculatePath(currentTile);
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            foreach (Tile neighbourTile in GetNeighbours(currentTile))
            {
                if (closedList.Contains(neighbourTile)) continue;

                int tentativeGCost = currentTile.gCost + DistanceCost(currentTile, neighbourTile);

                if(tentativeGCost < neighbourTile.gCost)
                {
                    neighbourTile.previus = currentTile;
                    neighbourTile.gCost = tentativeGCost;
                    neighbourTile.hCost = DistanceCost(neighbourTile, endTile);

                    if (!openList.Contains(neighbourTile))
                        openList.Add(neighbourTile);
                }
            }
                
        }

        return null;
    }

    

    private List<Tile> CalculatePath(Tile endTile)
    {
        List<Tile> path = new List<Tile>();
        path.Add(endTile);

        var currentTile = endTile;

        while(currentTile.previus != null)
        {
            path.Add(currentTile.previus);
            currentTile = currentTile.previus;
        }

        path.Reverse();

        return path;
    }

    private int DistanceCost(Tile a, Tile b)
    {
        int xDis = Mathf.Abs(a.x - b.x);
        int yDis = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDis - yDis);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDis, yDis) + MOVE_STRAIGHT_COST * remaining;
    }

    private Tile LowestFCostTile(List<Tile> pathTileList)
    {
        Tile lowestFCostTile = pathTileList[0];

        for(int i = 1; i < pathTileList.Count; i++)
        {
            if(pathTileList[i].FCost < lowestFCostTile.FCost)
                lowestFCostTile = pathTileList[i];
        }

        return lowestFCostTile;
    }

    private List<Tile> GetNeighbours(Tile tile)
    {
        List<Tile> neighbours = new List<Tile>();
        if (tile.x > 0)
        {
            neighbours.Add(_grid.GetTile(tile.x - 1, tile.y));
            if (tile.y > 0)
                neighbours.Add(_grid.GetTile(tile.x - 1, tile.y - 1));
            if (tile.y < _grid.Height)
                neighbours.Add(_grid.GetTile(tile.x - 1, tile.y + 1));
        }
        if (tile.x < _grid.Width)
        {
            neighbours.Add(_grid.GetTile(tile.x + 1, tile.y));
            if (tile.y > 0)
                neighbours.Add(_grid.GetTile(tile.x + 1, tile.y - 1));
            if (tile.y < _grid.Height)
                neighbours.Add(_grid.GetTile(tile.x + 1, tile.y + 1));
        }

        if (tile.y < _grid.Height)
            neighbours.Add(_grid.GetTile(tile.x, tile.y + 1));
        if (tile.y > 0)
            neighbours.Add(_grid.GetTile(tile.x, tile.y - 1));

        return neighbours;
    }

}
