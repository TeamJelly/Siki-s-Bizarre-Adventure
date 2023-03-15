using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneStarter : MonoBehaviour {

    public Transform startPos1;
    public Transform startPos2;
    public GameObject Player;
    public GameObject chatUI;
    private string sceneName;
	void Awake()
    {       
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "2nd Floor")
        {
            if (GameManager.Stage1.isPuzzleExit)
            {
                Player.transform.position = startPos2.position;
                Player.transform.rotation = startPos2.rotation;
                GameManager.Stage1.isPuzzleExit = false;
            }
            else
            {
                Player.transform.position = startPos1.position;
            }
            chatUI.SetActive(!GameManager.Stage1.CheckVisited(sceneName));
            GameManager.Stage1.SetVisitedFloor(sceneName);
        }
        else if(sceneName == "1st Floor")
        {
            chatUI.SetActive(!GameManager.Stage1.CheckVisited(sceneName));
            GameManager.Stage1.SetVisitedFloor(sceneName);
        }
        else if(SceneManager.GetActiveScene().name == "Puzzle")
        {
            GameManager.Stage1.isPuzzleExit = true;
        }

    }
}
