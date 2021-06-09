using UnityEngine;

public class Sample1 : MonoBehaviour
{
    GameObject[] m_array;
    int m_select;
    void Start()
    {
        m_array = new GameObject[5];

        for (int i = 0; i < m_array.Length; i++)
        {
            // 立方体を生成
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(-4 + i * 2, 0, 0);

            m_array[i] = cube;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_select++;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_select--;
        }

        if (m_select < 0)
        {
            m_select = 0;
        }
        if (m_select >= m_array.Length)
        {
            m_select = m_array.Length - 1;
        }

        for (int i = 0; i < m_array.Length; i++)
        {
            // Rendererからマテリアルの色を変更。先頭が赤、それ以外が白
            var r = m_array[i].GetComponent<Renderer>();
            r.material.color = (i == m_select ? Color.red : Color.white);
        }
    }
}
