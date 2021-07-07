using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    private float cellsize = 10f;
    private TileGrid _grid;
    private Pathfinding _pathfinding;

    // Start is called before the first frame update
    void Start()
    {
        _grid = new TileGrid(10, 10, cellsize);
        _pathfinding = new Pathfinding(_grid);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 inputWorldPosition = InputService.GetInputWorldPosition();
            (int x, int y) = _grid.GetCoordinates(inputWorldPosition);
            List<Tile> path = _pathfinding.FindPath(0, 0, x, y);

            if(path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.Log($"From ({path[i].x} , {path[i].y}) To ({path[i + 1].x}, {path[i + 1].y})");
                    Vector3 cellOffset = Vector3.one.normalized * cellsize;
                    Debug.DrawLine(_grid.GetWorldPosition(path[i].x, path[i].y) + cellOffset, _grid.GetWorldPosition(path[i + 1].x, path[i + 1].y) + cellOffset, Color.green, 5f);
                }   
            }
        }
    }
}
