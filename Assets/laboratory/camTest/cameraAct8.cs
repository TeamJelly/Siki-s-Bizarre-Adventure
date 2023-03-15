using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraAct8 : MonoBehaviour {
    public Transform standardPos;          // the usual position for the camera, specified by a transform in the game 
    public Transform reversStandard;
    private Transform goPos;
    public GameObject player;
    public float rotateSpeed = 3.0f;
    public float speedState;
    public float speed;
    private bool isMove = true;
    void Start()
    {
        //standardPos = GameObject.Find("CamPos").transform;
        transform.position = standardPos.position;
        transform.forward = standardPos.forward;
        //this.transform.forward는 world 기준 자신의 z축 포지션을 말하는것.

    }


    void FixedUpdate()
    {
        setCameraPositionNormalView();
    }

    void setCameraPositionNormalView()
    {
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        if (GameManager.isDie) isMove = true;
        if (dist > 13) isMove = true;

        if (isMove)
        {
            if (dist > 20) speedState = 40;
            else
            {
                if (playerMove8.isRunning || playerMove8.isJumping)
                {
                    speedState = 20;
                }
                else
                {
                    speedState = 13;
                }
                if ((playerMove8.LeftMove || playerMove8.RightMove) && !(playerMove8.FrontMove || playerMove8.BackMove) && dist < 15)
                {
                    if (playerMove8.isRunning) speedState = 10;
                    else speedState = 4;
                }

            }
            if (Vector3.Distance(standardPos.position, this.transform.position) > Vector3.Distance(reversStandard.position, this.transform.position))
                goPos = reversStandard;
            else goPos = standardPos;
            //캐릭터가 카메라를 향해 섰을때 뒤로 안가고 위치 유지.
            if (Input.GetButtonDown("f"))
            {
                transform.position = standardPos.position;
            }
            //f키 누르면 뒤 보기.
            if (speedState > speed) speed += Time.deltaTime * 15;
            else speed -= Time.deltaTime * 10;
            transform.position = Vector3.MoveTowards(transform.position, goPos.position, Time.deltaTime * speed);
        }

        Vector3 pos = player.transform.position - transform.position;
        Quaternion newRot = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, rotateSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.tag == ("Wall") /*|| other.gameObject.tag == ("Ground")*/) isMove = false;

    }
}
