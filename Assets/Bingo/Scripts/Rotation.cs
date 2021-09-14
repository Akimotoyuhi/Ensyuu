using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 3));
    }
}
