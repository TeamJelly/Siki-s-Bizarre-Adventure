using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadCtrl : MonoBehaviour {
    public GameObject cordinate;
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Player")
        {
            coll.transform.position = cordinate.transform.position;
        }
    } 
}
