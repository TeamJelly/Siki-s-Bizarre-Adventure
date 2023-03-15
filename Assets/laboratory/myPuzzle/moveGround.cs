using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGround : MonoBehaviour {

    Rigidbody rgb;
    public int dir = 1;
    public GameObject spaceA;
    public GameObject spaceB;
    Transform destination;
	/*void OnTriggerStay(Collider other)
    {
        if(other.tag == ("MovingGround"))
        {
            Debug.Log("?");
            rgb = other.GetComponent <Rigidbody> ();
            rgb.AddForce(new Vector3(0, dir, 0));
        }
    }*/
    void Start()
    {
        destination = spaceB.transform;
    }
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination.position, 3 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == ("spaceA"))
        {
            //Debug.Log("A");
            destination = spaceB.transform;
        }
        else if(other.name == ("spaceB"))
        {
            //Debug.Log("B");
            destination = spaceA.transform;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag == ("Player"))
        {
            other.transform.position += new Vector3(0,this.transform.position.y+0.4f-other.transform.position.y, 0);
        }
    }

}
