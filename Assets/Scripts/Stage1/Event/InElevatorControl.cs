using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InElevatorControl : MonoBehaviour {

	void OnTriggerStay(Collider other)
    {
        if(other.tag == ("Player"))
        {
            if (ElevatorControl.isClosed)
            {
                SceneManager.LoadScene("Siki'sRoom");
            }
        }
    }
}
