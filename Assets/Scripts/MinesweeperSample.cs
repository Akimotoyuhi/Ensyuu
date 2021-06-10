using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinesweeperSample : MonoBehaviour
{
    [SerializeField]
    private Cell _cellPrefab = null;

    [SerializeField]
    private GridLayoutGroup _container = null;

    private GameObject[,] m_array = new GameObject[5, 5];

    private void Start()
    {
        for (int i = 0; i < m_array.GetLength(0); i++)
        {
            for (int n = 0; n < m_array.GetLength(1); n++)
            {
                var cell = Instantiate(_cellPrefab);
                cell.transform.parent = _container.transform;
            }
        }
    }
}
