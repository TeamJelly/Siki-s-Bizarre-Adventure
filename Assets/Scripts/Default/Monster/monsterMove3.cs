using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monsterMove3 : MonoBehaviour
{

    public enum MonsterState { idle, trace, attack, die };
    public MonsterState monsterState = MonsterState.idle;
    public float speed = 20.0f;
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator _animator;

    private BoxCollider bodycollider;
    private SphereCollider AttackTrigger;

    public float traceDist = 10.0f;
    public float attackDist = 4.0f;

    public GameObject bloodEffect;
    public GameObject bloodDecal;

    [System.Serializable]
    public struct DelayList
    {
        public float ready;
        public float attack;
        public float end;
    };
    public DelayList delay;
    private bool isDie = false;

    void Start()
    {
        monsterTr = this.gameObject.GetComponent<Transform>();

        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

        _animator = this.gameObject.GetComponent<Animator>();

        bodycollider = this.gameObject.GetComponent<BoxCollider>();
        AttackTrigger = gameObject.GetComponentInChildren<SphereCollider>();

        StartCoroutine(this.CheckMonsterState());
        StartCoroutine(this.MonsterAction());



        nvAgent.speed = speed;
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);
            //Debug.Log("??");
            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if (dist <= attackDist)
            {
                if (GameManager.isDie) monsterState = MonsterState.idle;
                else monsterState = MonsterState.attack;
            }
            else if (dist <= traceDist)
            {
                monsterState = MonsterState.trace;
            }
            else
            {
                monsterState = MonsterState.idle;
            }

        }
    }


    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    //Debug.Log("idle");
                    gameObject.GetComponentInChildren<SphereCollider>().enabled = false;
                    nvAgent.Stop();
                    _animator.SetBool("Shoot", false);
                    _animator.SetBool("Run", false);
                    break;

                case MonsterState.trace:
                    //Debug.Log("trace");
                    gameObject.GetComponentInChildren<SphereCollider>().enabled = false;
                    nvAgent.Resume();
                    nvAgent.destination = playerTr.position;
                    _animator.SetBool("Shoot", false);
                    _animator.SetBool("Run", true);
                    break;
                case MonsterState.attack:
                    //Debug.Log("attack");
                    nvAgent.Stop();
                    StartCoroutine(LookRot());
                    _animator.SetBool("Run", false);
                    _animator.SetBool("Shoot", true);

                    yield return new WaitForSeconds(delay.ready);
                    AttackTrigger.enabled = true;
                    yield return new WaitForSeconds(delay.attack);
                    AttackTrigger.enabled = false;
                    yield return new WaitForSeconds(delay.end);
                    break;
            }
            yield return null;
        }
    }

    IEnumerator LookRot()
    {
        while (monsterState == MonsterState.attack && AttackTrigger.enabled == false)
        {
            transform.LookAt(playerTr);
            yield return new WaitForEndOfFrame();
        }
    }

    public void MonsterDie()
    {
        Debug.Log("monsterhit");
        bodycollider.enabled = false;
        StopAllCoroutines();
        isDie = true;
        monsterState = MonsterState.die;
        nvAgent.Stop();
        _animator.SetTrigger("isDie");
        AttackTrigger.enabled = false;
    }

    // /*
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player_Att")
        {
            CreateBloodEffect(coll.transform.position);
            Debug.Log("DIE!!");
            MonsterDie();
        }
    }
    // */

    public void CreateBloodEffect(Vector3 pos)
    {
        GameObject blood1 = (GameObject)Instantiate(bloodEffect, pos, Quaternion.identity);
        Destroy(blood1, 2.0f);

        Vector3 decalPos = monsterTr.position + (Vector3.up * 0.05f);
        Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0,360));

        GameObject blood2 = (GameObject)Instantiate(bloodDecal, decalPos, decalRot);
        float scale = Random.Range(1.5f, 3.5f);
        blood2.transform.localScale = Vector3.one * scale;

        Destroy(blood2, 3.0f);
    }
}