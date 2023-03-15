using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControl : MonoBehaviour {


    Animation anim;
    bool isOpened = false;
    public static bool isClosed = true;
	

    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player") && !isOpened && isClosed)
        {
            Debug.Log("animPlay");
            anim["autodoor"].speed = 1;
            anim.Play("autodoor");
            StartCoroutine(setDelay());
        }
    }

    IEnumerator setDelay()
    {
        isOpened = true;
        isClosed = false;
        yield return new WaitForSeconds(0.8f);
        anim["autodoor"].speed = 0;
        yield return new WaitForSeconds(2.3f);
        isOpened = false;
        anim["autodoor"].speed = -1;
        yield return new WaitForSeconds(0.9f);
        isClosed = true;
    }
}
