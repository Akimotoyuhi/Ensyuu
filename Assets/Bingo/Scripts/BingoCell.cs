using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BingoCellState
{
    close,
    open
}

public class BingoCell : MonoBehaviour
{
    private GameObject m_textobj;
    private Text m_text;
    private GameObject m_image;
    public int m_num;
    public BingoCellState m_bingoCellState = BingoCellState.close;

    void Start()
    {
        m_textobj = gameObject.transform.Find("Text").gameObject;
        m_text = m_textobj.GetComponent<Text>();
        m_image = gameObject.transform.Find("Image").gameObject;
        m_image.SetActive(false);

        CellStateChanged();
    }

    public void CellStateChanged()
    {
        if (m_bingoCellState == BingoCellState.close)
        {
            m_text.text = $"{m_num}";
        }
        else
        {
            m_textobj.SetActive(false);
            m_image.SetActive(true);
        }
    }
}
