using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberGenerator : MonoBehaviour
{
    private List<int> m_list = new List<int>();
    private Text m_text;
    private BingoGameManager m_gameManager;

    void Start()
    {
        GameObject obj = gameObject.transform.Find("Text").gameObject;
        m_text = obj.GetComponent<Text>();
        obj = GameObject.Find("Bingo");
        m_gameManager = obj.GetComponent<BingoGameManager>();
    }

    public void Generate()
    {
        int n = Random.Range(1, 100);
        m_list.Add(n);
        m_text.text = $"{n}";
        m_gameManager.GetNumber(n);
    }
}
