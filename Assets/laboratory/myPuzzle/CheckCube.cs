using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCube : MonoBehaviour {

    public int cubeNum;
    public GameObject Decoration;
	void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == ("PuzzleCube"))
        {
            Debug.Log(GameManager.Puzzle.currectCube);
            if (other.gameObject.GetComponent<CubeCtrl>().CubeNum == cubeNum)
            {
                GameManager.Puzzle.currectCube++;
                Decoration.SetActive(true);
            }
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == ("PuzzleCube"))
        {
            if (other.gameObject.GetComponent<CubeCtrl>().CubeNum == cubeNum)
            {
                GameManager.Puzzle.currectCube--;
                Decoration.SetActive(false);
            }
        }
    }
}
