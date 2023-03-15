using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraAct4 : MonoBehaviour {
    public Transform standardPos;          // the usual position for the camera, specified by a transform in the game 
    public GameObject player;
    public float rotateSpeed = 3.0f;
    float speed;
    private bool isMove = true;

    void Start()
    {
        //standardPos = GameObject.Find("CamPos").transform;
        transform.position = player.transform.position;
        transform.forward = standardPos.forward;
        //this.transform.forward는 world 기준 자신의 z축 포지션을 말하는것.
    }


    void FixedUpdate()
    {
        setCameraPositionNormalView();
    }

    void setCameraPositionNormalView()
    {
        rotateSpeed = 1f;
        float dist = Vector3.Distance(player.transform.position, this.transform.position);

        if (dist > 13) isMove = true;
        if (isMove)
        {
            if(dist > 20) speed = 40;
            if (dist > 13 && dist < 20)
            {
                if (!(playerMove5.isRunning)) speed = 16;
                else speed = 30;

                if (playerMove5.isJumping) speed = 10;                
            }
            else if (dist < 4)
            {
                rotateSpeed = 0.4f;
                speed = 4;
            }           
            else
            { if (playerMove5.isJumping)
                    speed = 20;
                else speed = 15;
            }
            transform.position = Vector3.MoveTowards(transform.position, standardPos.position, Time.deltaTime * speed);
        }
      
        Vector3 pos = player.transform.position - transform.position;
        Quaternion newRot = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, rotateSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.tag == ("Wall") || other.gameObject.tag == ("Ground")) isMove = false;
        else isMove = true;

    }


}
