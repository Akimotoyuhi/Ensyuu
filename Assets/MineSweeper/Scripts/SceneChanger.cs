using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private GameObject _gamemode;
    private GameObject _msLevelSelect;
    private AudioSource _as;
    [SerializeField] private AudioClip _se;
    void Start()
    {
        _gamemode = GameObject.Find("GameMode");
        _msLevelSelect = GameObject.Find("msLevelSelect");
        _msLevelSelect.SetActive(false);
        _as = GetComponent<AudioSource>();
    }
    public void EasyScene()
    {
        _as.PlayOneShot(_se);
        SceneManager.LoadScene("MinesweeperEasy");
    }

    public void NormalScene()
    {
        _as.PlayOneShot(_se);
        SceneManager.LoadScene("MinesweeperNormal");
    }

    public void HardScene()
    {
        _as.PlayOneShot(_se);
        SceneManager.LoadScene("MinesweeperHard");
    }

    public void MinesweeperButton()
    {
        _as.PlayOneShot(_se);
        _gamemode.SetActive(false);
        _msLevelSelect.SetActive(true);
    }

    public void MsBack()
    {
        _as.PlayOneShot(_se);
        _gamemode.SetActive(true);
        _msLevelSelect.SetActive(false);
    }
}
