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
    public bool _openFlag = false;
    private GameObject _player;
    Animator _pAnim = null;
    private GameObject _enemy;
    Animator _eAnim = null;

    private AudioSource _as = null;
    [SerializeField] private AudioClip _playerAttack;
    [SerializeField] private AudioClip _enemyAttack;
    [SerializeField] private AudioClip _deflag;
    [SerializeField] private AudioClip _adflag;

    public CellState CellState
    {
        get => _cellState;
        set
        {
            _cellState = value;
            OnCellStateChanged();
        }
    }

    //Action<Cell> _opened { get; set; }
    //public void AddOpened(action<Cell> opened);

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
        _player = GameObject.Find("Player");
        _pAnim = _player.GetComponent<Animator>();
        _enemy = GameObject.Find("Enemy");
        _eAnim = _enemy.GetComponent<Animator>();
        _as = GetComponent<AudioSource>();
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
        //既に開いているセルなら何もしない
        if (_openFlag)
        {
            return;
        }

        _openFlag = true;

        if (_cellState == CellState.Mine)
        {
            _as.PlayOneShot(_enemyAttack);
            _gameManager._playerLife--;
            _pAnim.SetTrigger("Damage");
        }
        else if (_enemy)
        {
            _as.PlayOneShot(_playerAttack);
            _gameManager._EnemyLife--;
            _eAnim.SetTrigger("Damage");
            _gameManager.EnemyDamageEffect();
        }
        GameObject button = transform.Find("Button").gameObject;
        Destroy(button);
    }
    /// <summary>
    /// クリックを検出してセル開けたり旗立てたりする
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
                //旗を消す
                _as.PlayOneShot(_deflag);
                text.SetActive(false);
                _textErase = false;
            }
            else
            {
                //旗を立てる
                _as.PlayOneShot(_adflag);
                text.SetActive(true);
                _textErase = true;
            }
        }
    }
}
