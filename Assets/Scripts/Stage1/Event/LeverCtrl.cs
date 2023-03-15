using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverCtrl : MonoBehaviour {

	Animation anim;

    EscalatorCtrl escalator;
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        escalator = GameObject.Find("Escalator Area").GetComponent<EscalatorCtrl>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            anim.Play("leaverdown");
            escalator.speed = 10;
        }
    }
}
