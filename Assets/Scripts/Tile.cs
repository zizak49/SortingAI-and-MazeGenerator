using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool _isWall = false;

    private int _gCost; 
    private int _hCost;
    private int _fCost;

    private int _posX;
    private int _posY;

    private List<Tile> _neighbours = new List<Tile>();
    private Tile _parent;

    public bool IsWall { get => _isWall; set => _isWall = value; }
    public int PosX { get => _posX; set => _posX = value; }
    public int PosY { get => _posY; set => _posY = value; }
    public int GCost { get => _gCost; set => _gCost = value; }
    public int HCost { get => _hCost; set => _hCost = value; }
    public int FCost { get => _gCost + HCost; }
    public Tile Parent { get => _parent; set => _parent = value; }
    public List<Tile> Neighbours { get => _neighbours; set => _neighbours = value; }

    [SerializeField] private Color _onMouseOver;
    private Color _color;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _color = _renderer.color;
    }  

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (_color == Color.white)
            {
                _renderer.color = Color.black;
                IsWall = true;
                UpdateNeighbours();
            }
            else
            {
                _renderer.color = Color.white;
                IsWall = false;
            }
        }

        if (Input.GetMouseButton(1))
        {
            GetComponentInParent<MazeManager>().SetStartEndTile(this);
        }
    }

    private void UpdateNeighbours() 
    {
        if (_isWall)
        {
            foreach (Tile item in _neighbours)
            {
                if (item.Neighbours.Contains(this))
                {
                    item.Neighbours.Remove(this);
                }
            }
        }
    }

    public void ColorStart() 
    {
        _renderer.color = Color.green;
    }

    public void ColorEnd() 
    {
        _renderer.color = Color.red;
    }

    public void ColorPath() 
    {
        _renderer.color = Color.white;
    }

    public void ColorVisited() 
    {
        _renderer.color = Color.yellow; 
    }
}
