using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetcubePos : MonoBehaviour {

    private bool isPlaced = false;
    Vector3 originTransform;
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == ("PuzzleCube") && !isPlaced)
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            other.transform.parent = this.transform;
            other.transform.position = this.transform.position + new Vector3(0,other.transform.position.y - this.transform.position.y,0);
            other.transform.rotation = this.transform.rotation;
            GameManager.Puzzle.isCubeCatched = false;
            GameManager.Puzzle.placedCube++;
            StartCoroutine(Moving());
            isPlaced = true;
        }
    }

    /*void OnCollisionExit(Collision other)
    {
        if(other.gameObject== attachedCube && isPlaced)
        {
            isPlaced = false;
        }
    }*/
    IEnumerator Moving()
    {
        originTransform = this.transform.position;
        float t = 0;
        while(t < 5f)
        {
            this.transform.position += new Vector3(0, Time.deltaTime*4, 0);
            t += Time.deltaTime*4;
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
    IEnumerator ReverseMoving()
    {
        float t = 5f;
        while (t > 0)
        {
            this.transform.position -= new Vector3(0, Time.deltaTime*4, 0);
            t -= Time.deltaTime*4;
            yield return new WaitForFixedUpdate();
        }
        this.transform.position = originTransform;
        yield return null;
    }
    void Update()
    {
        if (GameManager.Puzzle.resetMode)
        {
            if (isPlaced) StartCoroutine(ReverseMoving());
            isPlaced = false;
        }
    }
}
