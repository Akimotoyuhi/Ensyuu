using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    None = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,

    Mine = -1,
}

public class Cell : MonoBehaviour
{
    [SerializeField]
    private Text _view = null;

    [SerializeField]
    private CellState _cellState = CellState.None;

    private MinesweeperGameManager _gameManager = null;
    private GameObject _object = null;

    GameObject text = null;
    private bool _textErase = false;
    private bool _sudeniOwatta = false;
    public bool _openFlag = false;

    public CellState CellState
    {
        get => _cellState;
        set
        {
            _cellState = value;
            OnCellStateChanged();
        }
    }

    private void OnValidate()
    {
        // OnValidate関数を使用する事でUnityを実行しなくてもテキストの状態を確認する事が出来る
        OnCellStateChanged();
    }

    private void Start()
    {
        text = transform.Find("Button/Image").gameObject;
        text.SetActive(false);
        _object = GameObject.Find("Minesweeper");
        _gameManager = _object.GetComponent<MinesweeperGameManager>();
    }

    private void OnCellStateChanged()
    {
        if (_view == null)
        {
            return;
        }
        if (_cellState == CellState.None)
        {
            _view.text = "";
        }
        else if (_cellState == CellState.Mine)
        {
            _view.text = "X";
            _view.color = Color.red;
        }
        else
        {
            _view.text = ((int)_cellState).ToString();
            _view.color = Color.blue;
        }
    }
    /// <summary>
    /// セルを開ける
    /// </summary>
    /// <param name="flag"></param>
    public void IsOpen()
    {
        _openFlag = true;

        //既に開いているセルなら何もしない
        if (_sudeniOwatta)
        {
            return;
        }

        if (_cellState == CellState.Mine)
        {
            _gameManager._playerLife--;
        }
        else
        {
            _gameManager._EnemyLife--;
        }
        GameObject button = transform.Find("Button").gameObject;
        Destroy(button);
        _sudeniOwatta = true;
    }
    /// <summary>
    /// クリックを検出してセルを開けるフラグをオンにしたり旗立てたりする
    /// </summary>
    public void OnClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            IsOpen();
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (_textErase)
            {
                text.SetActive(false);
                _textErase = false;
            }
            else
            {
                text.SetActive(true);
                _textErase = true;
            }
        }
    }
}
