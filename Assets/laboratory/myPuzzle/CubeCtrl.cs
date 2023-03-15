using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCtrl : MonoBehaviour {

    public int CubeNum;
    Rigidbody rgb;
    Vector3 moveDir;
    private bool h;
    private Transform forward;
    void Start()
    {
        forward = GameObject.Find("Forward").GetComponent<Transform>();
        rgb = GetComponent<Rigidbody>();
    }
	void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == ("Player") && !GameManager.Puzzle.isCubeCatched && Input.GetButtonDown("Fire3"))
        {
            this.transform.SetParent(other.transform);
            rgb.isKinematic = true;
            transform.position = new Vector3(forward.position.x, this.transform.position.y+1, forward.position.z); ;
            GameManager.Puzzle.isCubeCatched = true;
            StartCoroutine(catched());
        }
    }
    IEnumerator catched()
    {
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Fire3")) yield return null;
        transform.SetParent(null);
        //this.transform.position += new Vector3(0, 8, 0);
        rgb.isKinematic = false;
        GameManager.Puzzle.isCubeCatched = false;
    }
    void OnDestroy()
    {
        GameManager.Puzzle.isCubeCatched = false;
        GameManager.Puzzle.placedCube = 0;
    }
}
