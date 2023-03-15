using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCtrl : MonoBehaviour {

    public GameObject cordinate;
    private GameObject cam;

    void Start()
    {
        cam = GameObject.Find("Camera");
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            StartCoroutine(Die(coll));
        }
        else if (coll.tag == "Trap")
            Destroy(coll.gameObject);
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