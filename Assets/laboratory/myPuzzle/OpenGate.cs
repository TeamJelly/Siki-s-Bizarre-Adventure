using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour {

    public GameObject Gate;
	void Update()
    {
        if(GameManager.Puzzle.currectCube == 4)
        {
            Gate.SetActive(true);

        }
        else Gate.SetActive(false);
    }
}
