using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalatorCtrl : MonoBehaviour {

    public float speed;

    void OnTriggerStay(Collider coll)
    {
        if(coll.tag == ("Player"))
        coll.transform.position += transform.forward * speed * Time.deltaTime;
    }
}