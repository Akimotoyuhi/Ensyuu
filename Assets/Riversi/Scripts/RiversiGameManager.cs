using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiversiGameManager : MonoBehaviour
{
    [SerializeField] private int _columns = 1;
    [SerializeField] private int _rows = 1;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup = null;
    [SerializeField] private RiversiCell _rcellPrefab = null;
    int _selectX = 0;
    int _selectY = 0;
    private RiversiCell[,] _rcells;

    private void OnValidate()
    {
        if (_columns < _rows)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = _columns;
        }
        else
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            _gridLayoutGroup.constraintCount = _rows;
        }
    }

    void Start()
    {
        var parent = _gridLayoutGroup.gameObject.transform;
        _rcells = new RiversiCell[_rows, _columns];

        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var cell = Instantiate(_rcellPrefab);
                cell.transform.SetParent(parent);
                _rcells[r, c] = cell;
            }
        }

        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                if (r == 3 && c == 4 || r == 4 && c == 3) { _rcells[r, c].RiversiCellState = RiversiCellState.Black; }
                if (r == 3 && c == 3 || r == 4 && c == 4) { _rcells[r, c].RiversiCellState = RiversiCellState.Write; }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _selectX++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _selectX--;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _selectY--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _selectY++;
        }

        for (int i = 0; i < _rcells.GetLength(0); i++)
        {
            for (int n = 0; n < _rcells.GetLength(1); n++)
            {
                var r = _rcells[i, n].GetComponent<Image>();
                r.color = (i == _selectY && n == _selectX ? Color.red : Color.green);
            }
        }
    }
}
