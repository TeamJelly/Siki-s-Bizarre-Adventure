using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class moveScene : MonoBehaviour {
    public string TargetScene;
    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == "Player")
        {
            SceneManager.LoadScene(TargetScene);
        }
    }
}
