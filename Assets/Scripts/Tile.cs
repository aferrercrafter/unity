using UnityEngine;

public class Tile
{
    private TileGrid _grid;
    private TextMesh _textMesh;
    private int _value;

    public int gCost;
    public int hCost;
    public int FCost { get { return gCost + hCost; } }
    public int x { get; set; }
    public int y { get; set; }

    public Tile previus;

    public int Value 
    { 
        get 
        { 
            return _value; 
        } 
        set
        {
            _value = value;
            _textMesh.text = _value.ToString();
        } 
    }

    public Tile(TileGrid grid, TextMesh textMesh, int x, int y)
    {
        _grid = grid;
        _textMesh = textMesh;
        this.x = x;
        this.y = y;
    }
}
