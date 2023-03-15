using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public int x, y, z;

    void Update()
    {
        transform.Rotate(new Vector3(x,y,z) * Time.deltaTime);
    }
}