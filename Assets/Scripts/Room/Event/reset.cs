using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour {

    Transform resetPlace;
    void Start()
    {
        resetPlace = GameObject.FindGameObjectWithTag("respawn").GetComponent<Transform>();
    }
	void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag == ("Player"))
        {
            Debug.Log("hi");
            other.gameObject.transform.position = resetPlace.transform.position;
        }
    }
}
