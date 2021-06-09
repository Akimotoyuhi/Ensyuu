using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Start()
    {
        
    }
    public void EasyScene()
    {
        SceneManager.LoadScene("MinesweeperEasy");
    }

    public void NormalScene()
    {
        SceneManager.LoadScene("MinesweeperNormal");
    }

    public void HardScene()
    {
        SceneManager.LoadScene("MinesweeperHard");
    }
}
