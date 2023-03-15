using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCube : MonoBehaviour {

    public GameObject cubes;
    public Transform cubeTransform;
    GameObject cloneCubes;
    //public static bool isMaked = false;

   void Start()
    {
        cloneCubes = GameObject.Instantiate(cubes,cubeTransform);
        cloneCubes.SetActive(true);
    }
	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("Player"))
        {
            StartCoroutine(reset());
        }
    }

    
    IEnumerator reset()
    {
        GameManager.Puzzle.resetMode = true;
        GameObject[] cubess = GameObject.FindGameObjectsWithTag("PuzzleCube");
        foreach (GameObject obj in cubess)Destroy(obj);
        cloneCubes = GameObject.Instantiate(cubes,cubeTransform);
        cloneCubes.SetActive(true);
        GameManager.Puzzle.placedCube = 0;
        GameManager.Puzzle.currectCube = 0;
        yield return new WaitForSeconds(0.1f);
        GameManager.Puzzle.resetMode = false;
    }
}
