using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    //ゲーム開始時のStartTextの表示切り替え
    //ゲーム開始後3秒後に消える

    float _time = 0f;
    float _destroyTime = 3f;

    void Update()
    {
        _time += Time.deltaTime;

        if (_time > _destroyTime)
        {
            IsDestroy();
        }
    }

    public void IsDestroy()
    {
        Destroy(this.gameObject);
    }
}
