using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BingoGameManager : MonoBehaviour
{
    /// <summary>�s�i�c�j</summary>
    [SerializeField]
    private int m_rows = 1;

    /// <summary>��i���j</summary>
    [SerializeField]
    private int m_columns = 1;

    [SerializeField]
    private GridLayoutGroup m_gridLayoutGroup = null;

    [SerializeField]
    private BingoCell m_cellPrefab = null;

    private BingoCell[,] m_cells;

    private int m_selectedNum;

    private void OnValidate()
    {
        if (m_columns < m_rows)
        {
            m_gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            m_gridLayoutGroup.constraintCount = m_columns;
        }
        else
        {
            m_gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            m_gridLayoutGroup.constraintCount = m_rows;
        }
    }

    void Start()
    {
        var parent = m_gridLayoutGroup.gameObject.transform;
        m_cells = new BingoCell[m_rows, m_columns];

        for (var r = 0; r < m_rows; r++)
        {
            for (var c = 0; c < m_columns; c++)
            {
                var cell = Instantiate(m_cellPrefab);
                cell.transform.SetParent(parent);
                int num = Random.Range(1, 100);
                cell.GetComponent<BingoCell>().m_num = num;
                m_cells[r, c] = cell;
            }
        }
    }

    /// <summary>
    /// NumberGenerator�ŏo���ԍ����󂯎��A�r���S�J�[�h�ɂ��̔ԍ������݂����炻��Cell�̏�Ԃ�Open�ɂ���
    /// </summary>
    /// <param name="num">�������ꂽ�ԍ�</param>
    public void GetNumber(int num)
    {
        for (int r = 0; r < m_rows; r++)
        {
            for (int c = 0; c < m_columns; c++)
            {
                //���������ԍ����r���S�J�[�h�ɂ������炻�̃Z�����J����
                if (m_cells[r, c].m_num == num)
                {
                    m_cells[r, c].m_bingoCellState = BingoCellState.open;
                    m_cells[r, c].CellStateChanged();

                    BingoChack(r, c);
                    /*
                    if (top >= 0)
                    {
                        if (left >= 0) { m_cells[top, left].IsOpen(); }
                        if (right < m_columns) { m_cells[top, right].IsOpen(); }
                        m_cells[top, c].IsOpen();
                    }
                    if (left >= 0) { m_cells[r, left].IsOpen(); }
                    if (right < m_columns) { m_cells[r, right].IsOpen(); }
                    if (bottom < m_rows)
                    {
                        if (left >= 0) { m_cells[bottom, left].IsOpen(); }
                        if (right < m_columns) { m_cells[bottom, right].IsOpen(); }
                        m_cells[bottom, c].IsOpen();
                    }
                    */
                }
            }
        }
    }

    public void BingoChack(int r, int c)
    {
        var left = c + 1;
        var right = c - 1;
        var top = r - 1;
        var bottom = r + 1;
        //�㒲�ׂ�
        if (top >= 0 && left >= 0)
        {

        }
        if (top >= 0 && right < m_columns)
        {

        }
        if (top >= 0)
        {
            if (m_cells[top, c].m_bingoCellState == BingoCellState.open)
            {

            }
        }
    }
}
