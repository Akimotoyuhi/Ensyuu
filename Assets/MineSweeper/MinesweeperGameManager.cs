using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MinesweeperGameManager : MonoBehaviour
{/// <summary>行（縦）</summary>
    [SerializeField]
    private int _rows = 1;

    /// <summary>列（横）</summary>
    [SerializeField]
    private int _columns = 1;

    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup = null;

    [SerializeField]
    private int _mineCount = 1;

    [SerializeField]
    private Cell _cellPrefab = null;

    private Cell[,] _cells;

    [SerializeField]
    public int _playerLife = 5;
    private GameObject _player;

    [SerializeField]
    public int _EnemyLife = 90;
    private GameObject _enemy;

    private GameObject _end;
    private bool _isEnd = false;
    private GameObject _winText;
    private GameObject _loseText;
    [SerializeField] private GameObject _damagePrefab = null;
    private Vector3 _vec;
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
    private void Start()
    {
        _player = GameObject.Find("Player");
        _enemy = GameObject.Find("Enemy");
        _end = GameObject.Find("End");
        _end.SetActive(false);
        _winText = GameObject.Find("WinText");
        _winText.SetActive(false);
        _loseText = GameObject.Find("LoseText");
        _loseText.SetActive(false);
        _vec = _enemy.transform.position;
        var parent = _gridLayoutGroup.gameObject.transform;
        _cells = new Cell[_rows, _columns];

        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var cell = Instantiate(_cellPrefab);
                cell.transform.SetParent(parent);
                _cells[r, c] = cell;
            }
        }
        //地雷の配置
        for (var i = 0; i < _mineCount; )
        {
            var r = Random.Range(0, _rows);
            var c = Random.Range(0, _columns);
            var cell = _cells[r, c];
            if (cell.CellState != CellState.Mine)
            {
                cell.CellState = CellState.Mine;
                AddMine(r, c);
                i++;
            }        
        }
    }

    private void Update()
    {
        AdjacentErase();
        GameEndChecker();
    }
    /// <summary>
    /// 地雷と隣接している8マスのCellStateを+1する
    /// </summary>
    /// <param name="r">rows</param>
    /// <param name="c">columns</param>
    private void AddMine(int r, int c)
    {
        var left = c - 1;
        var right = c + 1;
        var top = r - 1;
        var bottom = r + 1;

        if (top >= 0)
        {
            if (left >= 0 && _cells[top, left].CellState != CellState.Mine) {_cells[top, left].CellState++;}
            if (_cells[top, c].CellState != CellState.Mine) {_cells[top, c].CellState++; }
            if (right < _columns && _cells[top, right].CellState != CellState.Mine) {_cells[top, right].CellState++; }
        }
        if (left >= 0 && _cells[r, left].CellState != CellState.Mine) { _cells[r, left].CellState++; }
        if (right < _columns && _cells[r, right].CellState != CellState.Mine) { _cells[r, right].CellState++; }
        if (bottom < _rows)
        {
            if (left >= 0 && _cells[bottom, left].CellState != CellState.Mine) { _cells[bottom, left].CellState++; }
            if (_cells[bottom, c].CellState != CellState.Mine) { _cells[bottom, c].CellState++; }
            if (right < _columns && _cells[bottom, right].CellState != CellState.Mine) { _cells[bottom, right].CellState++; }
        }
    }
    private int GetMineCount(int r, int c)
    {
        var left = c - 1;
        var right = c + 1;
        var top = r - 1;
        var bottom = r + 1;
        
	    var count = 0;
        if (top >= 0)
        {
            if (left >= 0 && _cells[top, left].CellState == CellState.Mine) { count++; }
            if (_cells[top, c].CellState == CellState.Mine) { count++; }
            if (right < _columns && _cells[top, right].CellState == CellState.Mine) { count++; }
        }
        if (left >= 0 && _cells[r, left].CellState == CellState.Mine) { count++; }
        if (right < _columns && _cells[r, right].CellState == CellState.Mine) { count++; }
        if (bottom < _rows)
        {
            if (left >= 0 && _cells[bottom, left].CellState == CellState.Mine) { count++; }
            if (_cells[bottom, c].CellState == CellState.Mine) { count++; }
            if (right < _columns && _cells[bottom, right].CellState == CellState.Mine) { count++; }
        }
        return count;
    }

    /// <summary>
    /// 常時全体の空いているセルを探し、空いていて且つCellStateがnoneのセルの隣接８マスを開ける
    /// </summary>
    public void AdjacentErase()
    {
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _columns; c++)
            {
                if (_cells[r, c].CellState == CellState.None && _cells[r, c]._openFlag)
                {
                    var left = c - 1;
                    var right = c + 1;
                    var top = r - 1;
                    var bottom = r + 1;

                    if (top >= 0)
                    {
                        if (left >= 0) { _cells[top, left].IsOpen(); }
                        if (right < _columns) { _cells[top, right].IsOpen(); }
                        _cells[top, c].IsOpen();
                    }
                    if (left >= 0) { _cells[r, left].IsOpen(); }
                    if (right < _columns) { _cells[r, right].IsOpen(); }
                    if (bottom < _rows)
                    {
                        if (left >= 0) { _cells[bottom, left].IsOpen(); }
                        if (right < _columns) { _cells[bottom, right].IsOpen(); }
                        _cells[bottom, c].IsOpen();
                    }
                }
            }
        }
    }

    public void EnemyDamageEffect()
    {
        GameObject t =  Instantiate(_damagePrefab, _vec, _damagePrefab.transform.rotation);
        t.transform.SetParent(_enemy.transform);
    }

    /// <summary>
    /// ゲームの終了判定
    /// </summary>
    private void GameEndChecker()
    {
        if (_isEnd)
        {
            return;
        }

        if (_playerLife <= 0)
        {
            //敗北画面
            _end.SetActive(true);
            _loseText.SetActive(true);
            Destroy(_player);
            Destroy(_enemy);
            _isEnd = true;
        }
        if (_EnemyLife <= 0)
        {
            //勝利画面
            _end.SetActive(true);
            _winText.SetActive(true);
            Destroy(_player);
            Destroy(_enemy);
            _isEnd = true;
        }
    }
}
