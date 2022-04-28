
using UnityEngine;

[CreateAssetMenu]
public class CellsFactory : ScriptableObject
{
    [SerializeField] private Cell _cell;
    private Vector3 _scale;
    private Transform _parent;

    public Cell Get(Vector2Int positionOnBoard)
    {
        Cell cell = Instantiate(_cell,_parent);
        cell.transform.localScale = _scale;

        int startValue = Random.Range(0, 11) == 10 ? 1 : 0;
        cell.Initialize(positionOnBoard,startValue);

        return cell;
    }

    public Cell Get(Vector2Int positionOnBoard, int value)
    {
        Cell cell = Instantiate(_cell, _parent);
        cell.transform.localScale = _scale;

        cell.Initialize(positionOnBoard, value);

        return cell;
    }
    public void Initialize(Vector3 scale, Transform parent)
    { 
        _scale = scale;
        _parent = parent;
    }
}
