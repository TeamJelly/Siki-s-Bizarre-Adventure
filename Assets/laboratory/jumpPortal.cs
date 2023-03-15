using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPortal : MonoBehaviour {

    public float jumpPower = 30.0f;
	void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
}
