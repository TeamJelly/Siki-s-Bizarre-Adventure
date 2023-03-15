using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateShip : MonoBehaviour {

    public float rotateSpeed = 10.0f;

    // Use this for initialization
    public GameObject ship;
    void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Player")&& !GameManager.isGravityReverse)
            Rotate();

        
    }

    void Rotate()
    {
        if(Physics.gravity.y < 0)
        ship.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

        else ship.transform.Rotate(0, rotateSpeed * Time.deltaTime * -1, 0);
    }
}
