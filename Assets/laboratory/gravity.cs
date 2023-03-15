using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour {


    public float Gravity = -12;

    public float startSpeed = 0.0f;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.back * startSpeed);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Trap"))
        {
            Rigidbody rgb = other.GetComponent<Rigidbody>();
            Vector3 gravityUp = (other.transform.position - transform.position).normalized;
            Vector3 localUp = other.transform.up;
            rgb.AddForce(gravityUp * Gravity);
        }
        //Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * rgb.transform.rotation;
        //rgb.transform.rotation = Quaternion.Slerp(rgb.transform.rotation, targetRotation, 50f * Time.deltaTime);
    }

}
