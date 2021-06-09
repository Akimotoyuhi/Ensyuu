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
    private GameObject _player;
    Animator _pAnim = null;
    private GameObject _enemy;
    Animator _eAnim = null;


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
        // OnValidate�֐����g�p���鎖��Unity�����s���Ȃ��Ă��e�L�X�g�̏�Ԃ��m�F���鎖���o����
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
    /// �Z�����J����
    /// </summary>
    /// <param name="flag"></param>
    public void IsOpen()
    {
        _openFlag = true;

        //���ɊJ���Ă���Z���Ȃ牽�����Ȃ�
        if (_sudeniOwatta)
        {
            return;
        }

        if (_cellState == CellState.Mine)
        {
            _gameManager._playerLife--;
            _pAnim.SetTrigger("Damage");
        }
        else if (_enemy)
        {
            _gameManager._EnemyLife--;
            _eAnim.SetTrigger("Damage");
            _gameManager.EnemyDamageEffect();
        }
        GameObject button = transform.Find("Button").gameObject;
        Destroy(button);
        _sudeniOwatta = true;
    }
    /// <summary>
    /// �N���b�N�����o���ăZ�����J����t���O���I���ɂ���������Ă��肷��
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
