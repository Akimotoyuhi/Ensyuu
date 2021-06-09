using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    //�Q�[���J�n����StartText�̕\���؂�ւ�
    //�Q�[���J�n��3�b��ɏ�����

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
