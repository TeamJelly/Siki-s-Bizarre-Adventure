using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour {

    public float rotSpeed = 100.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
		transform.Rotate(Vector3.up* Time.deltaTime * rotSpeed);
    }
}
