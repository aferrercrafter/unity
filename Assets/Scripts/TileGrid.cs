using UnityEngine;

public class TileGrid
{
    private int _width;
    private int _height;

    private Tile[,] _grid;
    private Vector3 _originPos;

    private float _cellSize;

    public int Width { get { return _width; } }
    public int Height { get { return _height; } }

    public TileGrid(int width, int height, float cellSize, Vector3 originPos = default(Vector3))
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPos = originPos;

        _grid = new Tile[_width, _height];

        for(int x = 0; x < _width; x++)
            for(int y = 0; y < _height; y++)
            {
                _grid[x, y] = new Tile(this, Textservice.CreateWorldText("0", null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter), x, y);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    public Vector3 GetWorldPosition(int x,  int y)
    {
        return new Vector3(x, y) * _cellSize + _originPos;
    }

    private void SetValue(int x, int y, int value)
    {
        if(x < _width && y < _height && x >= 0 && y >= 0)
        {
            _grid[x, y].Value = value;
        }
    }

    public (int x, int y) GetCoordinates(Vector3 worldPos)
    {
        return (Mathf.FloorToInt((worldPos - _originPos).x / _cellSize), Mathf.FloorToInt((worldPos - _originPos).y / _cellSize));
    }

    public void SetValue(Vector3 worldPos, int value)
    {
        (int x, int y) = GetCoordinates(worldPos);
        SetValue(x, y, value);
    }

    public Tile GetTile(int x, int y)
    {
        return _grid[x, y];
    }

}
