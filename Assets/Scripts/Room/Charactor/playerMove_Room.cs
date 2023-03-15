using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove_Room : MonoBehaviour
{
    public float speed = 10;
    public float smooth = 3;
    public float jumpPower = 10f;
    private bool LeftMove = false;
    private bool RightMove = false;
    private bool FrontMove = false;
    private bool BackMove = false;

    private bool isJump = false;
    private bool isRun = false;
    Vector3 movement;
    Rigidbody rd;
    Animator animator;

    void Start()
    {
        movement.Set(0, 0, 0);
        rd = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }
    void LateUpdate()
    {
        LeftMove = Input.GetButton("LeftArray");
        RightMove = Input.GetButton("RightArray");
        BackMove = Input.GetButton("BackArray");
        FrontMove = Input.GetButton("FrontArray");
        if (LeftMove || RightMove || BackMove || FrontMove)
        {
            if (isJump) isRun = false;
            else isRun = true;
            setRun();
            Run();
        }
        else isRun = false;
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            Jump();
            isJump = true;
        }
        animatorUpdate();
    }

    void setRun()
    {
        if (FrontMove)
        {
            if (LeftMove) movement.Set(-10, 0, 10);
            else if (RightMove) movement.Set(10, 0, 10);
            else movement.Set(0, 0, 10);
        }
        else if (BackMove)
        {
            if (LeftMove) movement.Set(-10, 0, -10);
            else if (RightMove) movement.Set(10, 0, -10);
            else movement.Set(0, 0, -10);
        }
        else
        {
            if (LeftMove) movement.Set(-10, 0, 0);
            else if (RightMove) movement.Set(10, 0, 0);
        }
        movement = movement.normalized * speed * Time.deltaTime;
        Quaternion newRotation = Quaternion.LookRotation(movement);
        rd.rotation = Quaternion.Slerp(rd.rotation, newRotation, smooth * Time.deltaTime);
    }

    void Run()
    {
        Vector3 moveDir = (Vector3.forward);
        transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.Self);
    }

    void Jump()
    {
        rd.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == ("Ground"))
        {
            isJump = false;
        }
    }

    void animatorUpdate()
    {
            animator.SetBool("Run", isRun);
    }
}