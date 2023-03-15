using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove8 : MonoBehaviour {

    
    public float speed = 10;
    public float runSpeed = 15;
    public float rotSpeed = 20;
    public static float jumpPower = 10;
    public float smooth = 3f;

    public GameObject charactorModel;
    private Transform tr;
    private Rigidbody rd;
    private Vector3 movement;
    private Vector3 h_move;
    private Vector3 v_move;
    private Transform forward;//점프후 착지시에 앞방향을 알수 있도록 함.

    private bool isWalking = false;
    [System.NonSerialized]
    public static bool isJumping = false;
    [System.NonSerialized]
    public static  bool isAttack = false;
    [System.NonSerialized]
    public static bool isRunning = false;
    private Transform fPos;
    private Transform bPos;
    private Transform rPos;
    private Transform lPos;
    public static bool LeftMove = false;
    public static bool RightMove = false;
    public static bool FrontMove = false;
    public static bool BackMove = false;

    float key_attack = 0;
    float key_run = 0;

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

        if(GameObject.FindGameObjectWithTag("respawn"))
        respawnPos = GameObject.FindGameObjectWithTag("respawn").GetComponent<Transform>();
        camPos = GameObject.Find("Camera").GetComponent<Transform>();
        if(GameObject.Find("GameUI"))
        uiManager = GameObject.Find("GameUI").GetComponent<UIManager>();       
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isRunning = false;
            isWalking = false;
            isJumping = true;
            Jump();
        }
        key_run = Input.GetAxis("Run");
        key_attack = Input.GetAxis("Attack");
        /*if (Input.GetButtonDown("Attack") && !isAttack)
        {
            isAttack = true;
            isWalking = false;
            isRunning = false;
        }
        else isAttack = false;
        */
        animatorUpdate();
    }
    void FixedUpdate()
    {
        checkInput();
        
    }

    void checkInput()
    {
        if (!GameManager.isDie)
        {
            
            LeftMove = Input.GetButton("LeftArray");
            RightMove = Input.GetButton("RightArray");
            BackMove = Input.GetButton("BackArray");
            FrontMove = Input.GetButton("FrontArray");
            //Debug.Log(LeftMove);
            if (LeftMove || RightMove || BackMove || FrontMove)
            {
                setDestination();
                if (key_run == 1)
                {
                    if (!isJumping && !isAttack) isRunning = true;
                    isWalking = false;
                    Run();
                }
                else
                {
                    isRunning = false;
                    if (!isJumping && !isAttack) isWalking = true;
                    Walk();
                }
            }
            else
            {
                isRunning = false;
                isWalking = false;
            }
            if (key_attack == 1)
            {
                isWalking = false;
                isRunning = false;
                isAttack = true;
            }
            else isAttack = false;
        }
        else
        {
            isWalking = false;
            isRunning = false;
            isJumping = false;
        }
    }
    //방향 바꿔주기. v가 양수이면 전진, h가 양수이면 오른쪽으로.
    void setDestination()
    {
        v_move.Set(0, 0, 0);
        h_move.Set(0, 0, 0);
        if (FrontMove) v_move = fPos.position;
        else if(BackMove) v_move = bPos.position;
        if (LeftMove) h_move = lPos.position;
        else if (RightMove) h_move = rPos.position;
        movement = (v_move + h_move);
        if((v_move == fPos.position || v_move == bPos.position) && (h_move == lPos.position || h_move == rPos.position))
        {
            movement /= 2;
        } 
        movement.y = this.transform.position.y;
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
        rd.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    void animatorUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            //isAttack = true;
            AttackTrigger.enabled = true;
            //isWalking = false;
            //isRunning = false;
        }
        else
        {
            AttackTrigger.enabled = false;
            //isAttack = false;
        }
        animator.SetBool("Shoot", isAttack);
        animator.SetBool("Run", isRunning);
        animator.SetBool("Walk", isWalking);
        animator.SetBool("Die", GameManager.isDie);
        animator.SetBool("Jump", isJumping);
        
        //Debug.Log(isAttack);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == ("Trap"))
        {
            UIManager.DispLife(-1);
            StartCoroutine(Die());
        }
    }
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == ("Ground"))
        {
            isJumping = false;
            movement = forward.position;
            movement.y = transform.position.y;
        }
    }
    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == ("Ground"))
        isJumping = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Mon_Att") && !GameManager.isDie && !GameManager.isNoPain)
        {
            if (GameManager.powerMode)
            {
                StartCoroutine(Weaker());           
            }
            else
            {
                UIManager.DispLife(-1);
                StartCoroutine(Die());
            }
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

    IEnumerator Die()
    {
        GameManager.isDie = true;
        yield return new WaitForSeconds(2);
        if(GameManager.totLife <= 0)
        {
            uiManager.gameOver();
        }
        StartCoroutine(uiManager.popUp());
        yield return new WaitForSeconds(2);//죽은후 2초 기다림.

        //리스폰 장소로 이동.
        this.transform.position = respawnPos.position;
        camPos.position = this.transform.position;
        //yield return new WaitForSeconds(1);//리스폰 후 기다림
        GameManager.isDie = false;
        //StartCoroutine(CheckPlayerState());
        StartCoroutine(noDamage());
    }

    IEnumerator Weaker()
    {
        int count = 0;
        while(count != 10)
        {
            charactorModel.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            charactorModel.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            count++;
        }
        GameManager.powerMode = false;
        StartCoroutine(uiManager.popUp());
    }

    IEnumerator noDamage()
    {
        int count = 0;
        GameManager.isNoPain = true;
        while (count != 10)
        {
            charactorModel.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            charactorModel.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            count++;
        }
       GameManager.isNoPain = false;
    }

}
