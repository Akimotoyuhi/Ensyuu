using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample2 : MonoBehaviour
{
    GameObject[,] m_array;
    int m_selectX;
    int m_selectY;
    void Start()
    {
        m_array = new GameObject[5, 5];

        for (int i = 0; i < m_array.GetLength(0); i++)
        {
            for (int n = 0; n < m_array.GetLength(1); n++)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(-4 + n * 2, -3 + i * 2, 0);
                cube.name = $"array[{i}, {n}]";

                m_array[i, n] = cube;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_selectX++;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_selectX--;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_selectY++;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_selectY--;
        }

        if (m_selectX < 0)
        {
            m_selectX = 0;
        }
        if (m_selectX >= m_array.GetLength(1))
        {
            m_selectX = m_array.GetLength(1) - 1;
        }
        if (m_selectY < 0)
        {
            m_selectY = 0;
        }
        if (m_selectY >= m_array.GetLength(0))
        {
            m_selectY = m_array.GetLength(0) - 1;
        }
        //if (m_select < 0)
        //{
        //    m_select = 0;
        //}
        //if (m_select >= m_array.Length)
        //{
        //    m_select = m_array.Length - 1;
        //}
        for (int i = 0; i < m_array.GetLength(0); i++)
        {
            for (int n = 0; n < m_array.GetLength(1); n++)
            {
                var r = m_array[i, n].GetComponent<Renderer>();
                r.material.color = (i == m_selectY && n == m_selectX ? Color.red : Color.white);
            }
        }
    }
}
