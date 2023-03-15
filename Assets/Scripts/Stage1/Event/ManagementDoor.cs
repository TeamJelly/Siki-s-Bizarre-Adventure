using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagementDoor : MonoBehaviour {

    public GameObject StatusUI;
    public GameObject DefaultUI;
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == ("Player") && GameManager.Stage1.GetCardKey)
        {
            DefaultUI.SetActive(true);
            StatusUI.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
