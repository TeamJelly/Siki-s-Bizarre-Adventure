using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCtrl : MonoBehaviour {

    private Rigidbody rgb;

    void Start()
    {
        rgb = GetComponent<Rigidbody>();
    }
    void OnCollisionStay(Collision coll)
    {
        if (coll.collider.tag == "Player")
        {
            //Destroy(gameObject);
            //gameObject.SetActive(false);
            rgb.AddForce(Vector3.down*500);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == ("Trap"))
        {
            Destroy(this.gameObject);
        }
    }
}
