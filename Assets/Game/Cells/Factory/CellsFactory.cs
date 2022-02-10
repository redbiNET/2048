using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CellsFactory : ScriptableObject
{
    [SerializeField] private Cell _cell;
    private Vector3 _scale;

    public Cell Get(Vector2Int positionOnBoard, Transform parent)
    {
        Cell cell = Instantiate(_cell);
        cell.transform.SetParent(parent);
        cell.transform.localScale = _scale;

        int startValue = Random.Range(0, 11) == 10 ? 1 : 0;
        cell.Initialize(positionOnBoard,startValue);

        return cell;
    }
    public void Initialize(Vector3 scale)
    { 
        _scale = scale;
    }
}
