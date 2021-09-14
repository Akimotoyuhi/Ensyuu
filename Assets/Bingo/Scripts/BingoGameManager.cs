using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BingoGameManager : MonoBehaviour
{
    /// <summary>行（縦）</summary>
    [SerializeField]
    private int m_rows = 1;

    /// <summary>列（横）</summary>
    [SerializeField]
    private int m_columns = 1;

    [SerializeField]
    private GridLayoutGroup m_gridLayoutGroup = null;

    [SerializeField]
    private BingoCell m_cellPrefab = null;

    private BingoCell[,] m_cells;

    private bool m_isEnd = false;

    [SerializeField]
    private GameObject m_gameEndObj;

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
        m_gameEndObj.SetActive(false);
        var parent = m_gridLayoutGroup.gameObject.transform;
        m_cells = new BingoCell[m_rows, m_columns];

        //ビンゴカード作成
        for (var r = 0; r < m_rows; r++)
        {
            for (var c = 0; c < m_columns; c++)
            {
                var cell = Instantiate(m_cellPrefab);
                cell.transform.SetParent(parent);
                cell.name = $"{r} {c}";
                int num = Random.Range(1, 100);
                cell.GetComponent<BingoCell>().m_num = num;
                m_cells[r, c] = cell;
            }
        }
    }

    /// <summary>
    /// NumberGeneratorで出た番号を受け取り、ビンゴカードにその番号が存在したらそのCellの状態をOpenにする
    /// </summary>
    /// <param name="num">生成された番号</param>
    public void GetNumber(int num)
    {
        for (int r = 0; r < m_rows; r++)
        {
            for (int c = 0; c < m_columns; c++)
            {
                //生成した番号がビンゴカードにあったらそのセルを開ける
                if (m_cells[r, c].m_num == num)
                {
                    m_cells[r, c].m_bingoCellState = BingoCellState.open;
                    m_cells[r, c].CellStateChanged();

                    HeightBingoCheck(0, c);
                    SideBingoCheck(r, 0);
                    LeftCrossBingoCheck();
                    RightCrossBingoCheck();
                }
            }
        }
    }

    private void HeightBingoCheck(int r, int c)
    {
        int bottom = r + 1;

        if (m_cells[r, c].m_bingoCellState == BingoCellState.open)
        {
            if (bottom < m_rows)
            {
                HeightBingoCheck(bottom, c);
            }
            else
            {
                GameEnd();
            }
        }
    }

    private void SideBingoCheck(int r, int c)
    {
        int left = c + 1;

        if (m_cells[r, c].m_bingoCellState == BingoCellState.open)
        {
            if (left < m_columns)
            {
                HeightBingoCheck(r, left);
            }
            else
            {
                GameEnd();
            }
        }
    }

    private void LeftCrossBingoCheck(int r = 0, int c = 0)
    {
        int bottom = r + 1;
        int right = c + 1;

        if (m_cells[r, c].m_bingoCellState == BingoCellState.open)
        {
            if (bottom < m_rows && right < m_columns)
            {
                HeightBingoCheck(bottom, right);
            }
            else
            {
                GameEnd();
            }
        }
    }

    private void RightCrossBingoCheck(int r = 0, int c = 4)
    {
        int bottom = r + 1;
        int left = c - 1;

        if (m_cells[r, c].m_bingoCellState == BingoCellState.open)
        {
            if (bottom < m_rows && left >= 0)
            {
                HeightBingoCheck(bottom, left);
            }
            else
            {
                GameEnd();
            }
        }
    }

    private void GameEnd()
    {
        if (m_isEnd)
        {
            return;
        }
        m_isEnd = true;
        m_gameEndObj.SetActive(true);
    }
}
