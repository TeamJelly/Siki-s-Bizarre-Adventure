using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterAttack : MonoBehaviour {

    public GameObject cordinate;
    private GameObject cam;

    void Start()
    {
        cam = GameObject.Find("Camera");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("Player"))
        {
            other.transform.position += transform.position - other.transform.position;
            if (GameManager.powerMode)
            {
                GameManager.powerMode = false;
            }
            else Die(other);
        }
    }
    IEnumerator Die(Collider coll)
    {
        yield return new WaitForSeconds(2);
        GoPortal(coll);
        cam.transform.position = coll.transform.position;
        GameManager.isDie = false;
    }
    public void GoPortal(Collider coll)
    {
        coll.transform.position = cordinate.transform.position;
    }

}
