using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageImageRemoved : MonoBehaviour
{
    float time = 0;

    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            Destroy(this.gameObject);
        }
    }
}
