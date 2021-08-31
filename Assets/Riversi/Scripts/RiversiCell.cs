using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RiversiCellState
{
    None,
    Write,
    Black
}
public class RiversiCell : MonoBehaviour
{
    [SerializeField] private Text _view = null;
    [SerializeField] private RiversiCellState _rcellState = RiversiCellState.None;
    public RiversiCellState RiversiCellState
    {
        get => _rcellState;
        set
        {
            _rcellState = value;
            OnCellStateChanged();
        }
    }

    private void OnValidate()
    {
        OnCellStateChanged();
    }

    void Start()
    {
        
    }

    private void OnCellStateChanged()
    {
        if (_view == null)
        {
            return;
        }
        if (_rcellState == RiversiCellState.None)
        {
            _view.text = "";
            _view.color = Color.green;
        }
        if (_rcellState == RiversiCellState.Write)
        {
            _view.text = "Åú";
            _view.color = Color.white;
        }
        if (_rcellState == RiversiCellState.Black)
        {
            _view.text = "Åú";
            _view.color = Color.black;
        }
    }
}
