using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove5 : MonoBehaviour {

    

    public float speed = 10;
    public float runSpeed = 15;
    public float rotSpeed = 20;
    public static float jumpPower = 10;
    public float smooth = 3f;

    private Transform tr;
    private Rigidbody rd;
    private Vector3 movement;
    private Transform forward;//점프후 착지시에 앞방향을 알수 있도록 함.
    
    private bool isWalking = false;
    [System.NonSerialized]
    public static bool isJumping = false;
    [System.NonSerialized]
    public static bool isAttack = false;
    private bool isAttackAnim = false;
    [System.NonSerialized]
    public static bool isRunning = false;
    private bool isRunningAnim = false;
    private Transform fPos;
    private Transform bPos;
    private Transform rPos;
    private Transform lPos;
    private bool LeftMove = false;
    private bool RightMove = false;
    private bool FrontMove = false;
    private bool BackMove = false;

    private SphereCollider AttackTrigger;

    Animator animator;
    private Transform respawnPos;
    private Transform camPos;
    private UIManager uiManager;

    void Start()
    {
        fPos = GameObject.Find("Front").GetComponent<Transform>();
        bPos = GameObject.Find("Back").GetComponent<Transform>();
        lPos = GameObject.Find("Left").GetComponent<Transform>();
        rPos = GameObject.Find("Right").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        AttackTrigger = gameObject.GetComponentInChildren<SphereCollider>();
        tr = GetComponent<Transform>();
        rd = GetComponent<Rigidbody>();
        forward = GameObject.Find("Forward").GetComponent<Transform>();

        respawnPos = GameObject.FindGameObjectWithTag("respawn").GetComponent<Transform>();
        camPos = GameObject.Find("Camera").GetComponent<Transform>();
        uiManager = GameObject.Find("GameUI").GetComponent<UIManager>();
    }

    void FixedUpdate()
    {
        if (GameManager.isDie) return;
        LeftMove = Input.GetButton("LeftArray");
        RightMove = Input.GetButton("RightArray");
        BackMove = Input.GetButton("BackArray");
        FrontMove = Input.GetButton("FrontArray");
        //Debug.Log(LeftMove);

        if (Input.GetAxis("Run") == 1)
        {
            isRunning = true;

        }
        else isRunning = false;



        if (LeftMove || RightMove || BackMove || FrontMove)
        {
            if (isJumping || isRunning) isWalking = false;
            else isWalking = true;
            setDestination();
            if (isRunning)
            {
                if (!isAttackAnim)
                    isRunningAnim = true;
                Run();
            }
            else
            {
                Walk();
                isRunningAnim = false;
            }
        }
        else
        {
            isWalking = false;
            isRunning = false;
            isRunningAnim = false;
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            Jump();
        }
        if (isJumping) isRunningAnim = false;

        if (Input.GetButtonDown("Attack"))
        {
            isRunningAnim = false;
            isAttackAnim = true;
            isWalking = false;
            isJumping = false;
        }
        else isAttackAnim = false;
        
        animatorUpdate();
    }

    //방향 바꿔주기. v가 양수이면 전진, h가 양수이면 오른쪽으로.
    void setDestination()
    {
        if (FrontMove)
        {
            if (RightMove)
            {
                movement.Set((fPos.position.x + rPos.position.x) / 2, this.transform.position.y, (fPos.position.z + rPos.position.z) / 2);
            }
            else if (LeftMove)
            {
                movement.Set((fPos.position.x + lPos.position.x) / 2, this.transform.position.y, (fPos.position.z + lPos.position.z) / 2);
            }
            else movement.Set(fPos.position.x, this.transform.position.y, fPos.position.z);
        }

        else if (BackMove)
        {
            if (RightMove)
            {
                movement.Set((bPos.position.x + rPos.position.x) / 2, this.transform.position.y, (bPos.position.z + rPos.position.z) / 2);
            }
            else if (LeftMove)
            {
                movement.Set((bPos.position.x + lPos.position.x) / 2, this.transform.position.y, (bPos.position.z + lPos.position.z) / 2);
            }
            else movement.Set(bPos.position.x, this.transform.position.y, bPos.position.z);
        }
        else
        {
            if (RightMove) movement.Set(rPos.position.x, this.transform.position.y, rPos.position.z);
            else if (LeftMove) movement.Set(lPos.position.x, this.transform.position.y, lPos.position.z);
        }
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = Quaternion.LookRotation(movement - transform.position).eulerAngles;
        //rotation.x = 0;
        //rotation.z = 0;
        rotation.x = transform.eulerAngles.x;
        rotation.z = transform.eulerAngles.z;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smooth);
        
        

    }

    void Walk()
    {
        Vector3 moveDir = (Vector3.forward);
        tr.Translate(moveDir.normalized * speed * Time.deltaTime, Space.Self);
    }
    void Run()
    {
        Vector3 moveDir = (Vector3.forward);
        tr.Translate(moveDir.normalized * runSpeed * Time.deltaTime, Space.Self);
    }

    void Jump()
    {
        isJumping = true;
        rd.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    void animatorUpdate()
    {
        animator.SetBool("Shoot", isAttackAnim);
        animator.SetBool("Run", isRunningAnim);
        animator.SetBool("Walk", isWalking);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            isAttack = true;
            AttackTrigger.enabled = true;
        }
        else
        {
            isAttack = false;
            AttackTrigger.enabled = false;
        }
        //Debug.Log(isAttack);
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == ("Ground") || other.gameObject.tag == ("Monster"))
        {
            isJumping = false;
            movement = forward.position;
            movement.y = transform.position.y;
        }
    }
    void OnCollisionExit(Collision other)
    {
        isJumping = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Mon_Att") && !GameManager.isDie)
        {
            if (GameManager.powerMode)
            {
                GameManager.powerMode = false;
                StartCoroutine(uiManager.popUp());
                //StartCoroutine(noPain());


                Weaker();
            }
            else
            {
                UIManager.DispLife(-1);
                StartCoroutine(Die());
            }
        }

        else if(other.gameObject.tag == ("Trap"))
        {
            UIManager.DispLife(-1);
            StartCoroutine(Die());
        }
        else if (other.gameObject.tag == ("LIFE"))
        {
            Destroy(other.gameObject);
            UIManager.DispLife(1);


        }
        else if (other.gameObject.tag == ("COIN"))
        {
            Destroy(other.gameObject);
            UIManager.DispCoin(1);
        }

        else if (other.gameObject.name == ("Card Key"))
        {
            GameManager.Stage1.GetCardKey = true;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == ("POWER"))
        {
            Destroy(other.gameObject);
            Debug.Log("Stronger");
            GameManager.powerMode = true;
            StartCoroutine(uiManager.popUp());
            Stronger();
        }
        
    }

    void Stronger()
    {

    }

    void Weaker()
    {

    }

    IEnumerator Die()
    {
        Debug.Log("Die!");
        GameManager.isDie = true;
        StartCoroutine(uiManager.popUp());
        yield return new WaitForSeconds(2);//죽은후 2초 기다림.

        //리스폰 장소로 이동.
        this.transform.position = respawnPos.position;
        camPos.position = this.transform.position;
        yield return new WaitForSeconds(1);//리스폰 후 기다림
        GameManager.isDie = false;
    }

    IEnumerator noPain()
    {
        yield return new WaitForSeconds(3f);
        GameManager.powerMode = false;


    }
}
