
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int Size { get; private set; }

    private Cell[,] _cells;

    [SerializeField] private RectTransform _emptyCell;
    [SerializeField] private RectTransform _backGround;

    [SerializeField] private SwipeHandler _swipeHandler;
    [SerializeField] private CellsFactory _cellsFactory;

    private BoardKeeper _boardKeeper;

    private void Start()
    {
        SpawnRandomCell();        
    }
    private void MoveCells(Vector2Int delta)
    {
        int dir = delta.x != 0 ? delta.x : delta.y;
        int startIndex = dir < 0 ? 1 : Size - 2;
        int movedCell = 0;

        for (int x = 0; x < Size; x++)
        {
            for (int y = startIndex; y<Size && y>=0; y-= dir)
            {
                var cell = delta.y != 0 ? _cells[x, y] : _cells[y, x];
                if (cell)
                {                
                    movedCell += MoveCell(cell, delta);
                }

            }
        }
        UpdateCells();
        if (movedCell > 0) SpawnRandomCell();
        else if (!CheckMovePossibility()) Game.LoseGame();
    }
    private int MoveCell(Cell cell, Vector2Int delta)
    {
        var startX = cell.Position.x + delta.x;
        var startY = cell.Position.y + delta.y;
        for (int x = startX,y = startY; x < Size && x>= 0 && y < Size && y >= 0; x+= delta.x, y+=delta.y)
        {
            Vector2Int nextPosition;
            var cellToMerge = _cells[x, y];
            if(cellToMerge)
            {
                _cells[cell.Position.x, cell.Position.y] = null;
                if (!cell.TryMerge(cellToMerge))
                {           
                    _cells[x - delta.x, y - delta.y] = cell;
                    nextPosition = new Vector2Int(x - delta.x, y - delta.y);
                    if (nextPosition == cell.Position) return 0;
                    cell.Move(nextPosition);

                }
                return 1;
            }
            if (x + delta.x == Size || x + delta.x < 0 || y + delta.y == Size || y + delta.y < 0)
            {
                _cells[cell.Position.x, cell.Position.y] = null;
                _cells[x, y] = cell;
                cell.Move(new Vector2Int(x, y));
                return 1;
            }
        }
        return 1;
    }

    private void SpawnRandomCell()
    {
        List<Vector2Int> voidPositions = new List<Vector2Int>();
        for(int x =0; x < Size; x++) 
        {
            for (int y = 0; y < Size; y++)
            {
                if (!_cells[x, y])
                {
                    voidPositions.Add(new Vector2Int(x, y));
                }
            }
        }
        if (voidPositions.Count == 0)
        {
            Game.LoseGame();
            return;
        }
        Vector2Int position = voidPositions[Random.Range(0, voidPositions.Count)];
        Cell cell = _cellsFactory.Get(position);
        _cells[position.x, position.y] = cell;
        
    }
    private void UpdateCells()
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                if (_cells[x, y])
                {
                    _cells[x, y].IsMerged = false;
                }
            }
        }
    }
    public void Initialize(int size)
    {
        Size = size;
        _cells = new Cell[Size,Size];
        Vector3 scale = _backGround.localScale / Size;
        _cellsFactory.Initialize(scale, transform);
        float step = scale.x * _backGround.sizeDelta.x;
        for (float x = step/2,i =0 ; i < Size; x+=step, i+= 1)
        {
            for (float y = step/2,j =0; j < Size; y+= step, j += 1)
            {
                GameObject cell = Instantiate(_emptyCell.gameObject, transform);
                cell.transform.localPosition = new Vector3(x, y,1);
                cell.transform.localScale = scale;
            }
        }
        _boardKeeper = new BoardKeeper(_cellsFactory);
        _boardKeeper.SetBoard(_cells);
        _swipeHandler.SetAction(MoveCells);
    }
    public void Save()
    {
        _boardKeeper?.SaveBoard(_cells);
    }
    private bool CheckMovePossibility()
    {
        if (!_cells[0, 0]) return true;
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                if ((y<Size-1 && (!_cells[x, y + 1] || _cells[x, y].Value == _cells[x, y + 1].Value)) ||
                    (x<Size-1 && (!_cells[x + 1, y] || _cells[x, y].Value == _cells[x + 1, y].Value))) return true;
            }
        }
        return false;
    }
}
