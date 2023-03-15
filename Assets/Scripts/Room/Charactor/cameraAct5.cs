using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraAct5 : MonoBehaviour {

    public Transform standardPos;
    public GameObject player;
    public float rotateSpeed = 3;

    void LateUpdate()
    {
        Vector3 pos = player.transform.position - transform.position;
        Quaternion newRot = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, rotateSpeed);
        //transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime);
    }

    IEnumerator Start()
    {
         float dist = Vector3.Distance(player.transform.position, this.transform.position);
        while (!(dist < 0.1))
        {
            //Debug.Log("Coroutin");
            transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime);
            dist = Vector3.Distance(player.transform.position, this.transform.position);
            yield return new WaitForEndOfFrame();
        }
    }
}
