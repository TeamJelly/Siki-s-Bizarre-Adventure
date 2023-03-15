using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChatUI : MonoBehaviour {

    [System.Serializable]
    public class chatter
    {
        public int who;
        public string talkString;
        
    };  
    public GameObject chatUI;
    public Text chatTxt;
    public GameObject[] Chatter;
 

    public chatter[] chatList;


    public bool startMessage;
    public bool MoveScene;
    private bool isSkipped = false;

    //private GameObject defaultUI;
    void Start()
    {
        //defaultUI = GameObject.Find("DefaultUI");
        if (startMessage) StartCoroutine(StartChat());
        if(SceneManager.GetActiveScene().name == "FirstIntro")
        {
            GameManager.Tutorial.isFirstGame = 1;
        }
    }

    void OnEnable()
    {
        StartCoroutine(StartChat());
    }
    IEnumerator StartChat()
    {
        //defaultUI.SetActive(false);
        
        int checkNum = 0;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0;
        chatUI.SetActive(true);
        while (checkNum < chatList.Length && !isSkipped)
        {
            chatTxt.text = chatList[checkNum].talkString;
            Chatter[chatList[checkNum].who].SetActive(true);
            while (!Input.GetButtonDown("Submit") && !isSkipped)
            {       
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            Chatter[chatList[checkNum].who].SetActive(false);
            checkNum++;                                          
        }

        chatUI.SetActive(false);
        Time.timeScale = 1;
        if (MoveScene) SceneManager.LoadScene("Siki'sRoom");
        //defaultUI.SetActive(true);
    }

    public void SkipChatt()
    {
        isSkipped = true;
    }
}
