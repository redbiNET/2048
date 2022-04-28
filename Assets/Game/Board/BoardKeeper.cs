
using UnityEngine;
public class BoardKeeper
{
    [SerializeField] private CellsFactory _cellsFactory;

    public BoardKeeper(CellsFactory cellsFactory)
    {
        _cellsFactory = cellsFactory;
    }
    public void SaveBoard(Cell[,] cells)
    {
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(0); y++)
            {
                if(cells[x,y])
                PlayerPrefs.SetInt($"{x}{y}{cells.Length}", cells[x, y].Value);
            }
        }
    }
    public void SetBoard(Cell[,] cells)
    {
        for (int x = 0; x < cells.Length; x++)
        {
            for (int y = 0; y < cells.Length; y++)
            {
                string key = $"{x}{y}{cells.Length}";
                if (PlayerPrefs.HasKey(key))
                {
                    var cell = _cellsFactory.Get(new Vector2Int(x, y), PlayerPrefs.GetInt(key));
                    cells[x, y] = cell;

                }
                PlayerPrefs.DeleteKey(key);
            }
        }
    }
    public static void ClearSave(int size)
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                PlayerPrefs.DeleteKey($"{x}{y}{size}");
            }
        }
    }
}