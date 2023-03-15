using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityChanger : MonoBehaviour {

    public GameObject player;
    public GameObject cam;
    public float changeDelay = 5;
    public static bool isActivate = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.tag == "Player" && !isActivate && GameManager.Puzzle.placedCube == 4)
        {
            isActivate = true;
            StartCoroutine(RotateChar(1));
            
        }
    }


    IEnumerator RotateChar(float duration)
    {
        float startRotation = player.transform.localEulerAngles.z;
        float endRotation = startRotation + 180.0f;
        float t = 0.0f;
        //Physics.gravity = new Vector3(0, Physics.gravity.y * -0.01f, 0);
        yield return new WaitForSeconds(changeDelay);
        Physics.gravity = new Vector3(0, Physics.gravity.y *-1, 0);
        yield return new WaitForSeconds(1);
        

        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            player.transform.localEulerAngles = new Vector3(0, 0,zRotation);
            cam.transform.localEulerAngles = new Vector3(0, 0, zRotation);
            player.transform.localPosition = new Vector3(0,(Mathf.Cos(zRotation/180*Mathf.PI)-1)*-1, 0);
            yield return null;
        }
        playerMove8.jumpPower *= -1;
        isActivate = false;
        yield return new WaitForSeconds(1);
    }

   /* void Update()
    {
        if (Input.GetButtonDown("Fire3") && !isActivate)
        {
            isActivate = true;
            StartCoroutine(RotateChar(1));
        }
    }*/
}
