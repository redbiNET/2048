using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] [Range(2,20)] private int _size;

    private Cell[,] _cells;

    [SerializeField] private RectTransform _emptyCell;
    [SerializeField] private RectTransform _backGround;

    [SerializeField] private SwipeHandler _swipeHandler;
    [SerializeField] private CellsFactory _cellsFactory;
    private void Awake()
    {
        _swipeHandler.SetAction(MoveCells);
        SetBoard();
    }
    private void Start()
    {
        GetRandomCell();        
    }
    private void MoveCells(Vector2Int delta)
    {
        int dir = delta.x != 0 ? delta.x : delta.y;
        int startIndex = dir < 0 ? 0 : _size - 1;

        for (int x = 0; x < _size; x++)
        {
            for (int y = startIndex; y<_size && y>=0; y-= dir)
            {
                var cell = delta.y != 0 ? _cells[x, y] : _cells[y, x];
                if (cell)
                {                
                    MoveCell(cell, delta);
                }

            }
        }
        UpdateCells();
        GetRandomCell();
    }
    private void MoveCell(Cell cell, Vector2Int delta)
    {
        var startX = cell.Position.x + delta.x;
        var startY = cell.Position.y + delta.y;
        for (int x = startX,y = startY; x < _size && x>= 0 && y < _size && y >= 0; x+= delta.x, y+=delta.y)
        {
            var cellToMerge = _cells[x, y];
            if(cellToMerge)
            {
                _cells[cell.Position.x, cell.Position.y] = null;
                if (!cell.TryMerge(cellToMerge))
                {           
                    _cells[x - delta.x, y - delta.y] = cell;

                    cell.Move(new Vector2Int(x - delta.x, y - delta.y));
                }
                return;
            }
            if (x + delta.x == _size || x + delta.x < 0 || y + delta.y == _size || y + delta.y < 0)
            {
                _cells[cell.Position.x, cell.Position.y] = null;
                _cells[x, y] = cell;
                cell.Move(new Vector2Int(x, y));

            }
        }
    }

    private void GetRandomCell()
    {
        List<Vector2Int> voidCell = new List<Vector2Int>();
        for(int x =0; x < _size; x++) 
        {
            for (int y = 0; y < _size; y++)
            {
                if (!_cells[x, y])
                {
                    voidCell.Add(new Vector2Int(x, y));
                }
            }
        }
        if (voidCell.Count == 0)
        {
            Game.LoseGame();
            return;
        }

        Vector2Int position = voidCell[Random.Range(0, voidCell.Count)];
        Cell cell = _cellsFactory.Get(position, transform);
        _cells[position.x, position.y] = cell;
        
    }
    private void UpdateCells()
    {

        for (int x = 0; x < _size; x++)
        {
            for (int y = 0; y < _size; y++)
            {
                if (_cells[x, y])
                {
                    _cells[x, y].IsMerged = false;
                }
            }
        }
    }
    private void SetBoard()
    {
        _cells = new Cell[_size,_size];
        Vector3 scale = _backGround.localScale / (_size);
        _cellsFactory.Initialize(scale);        
        _emptyCell.localScale = scale;
        float _step = scale.x * _backGround.sizeDelta.x;
        for (float x = _step/2,i =0 ; i < _size; x+=_step, i+= 1)
        {
            for (float y = _step/2,j =0; j < _size; y+= _step, j += 1)
            {
                GameObject cell = Instantiate(_emptyCell.gameObject, transform);
                cell.transform.localPosition = new Vector3(x, y,1);
            }
        }
    }
}
