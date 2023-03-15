using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour {

    private monsterMove3 monster;

    void Start()
    {
        monster = GetComponentInParent<monsterMove3>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player_Att")
        {
            Debug.Log("playeratt");
            monster.CreateBloodEffect(other.transform.position);
            monster.MonsterDie();
        }
    }
}
