using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour {

    public GameObject MenuUI;
    public GameObject DefaultUI;
    public GameObject ExitUI;
    public GameObject IntroUI;


    void Start()
    {
        if(GameManager.Tutorial.isFirstGame == 0)
        {
            SceneManager.LoadScene("FirstIntro");
        }
        else if(GameManager.Tutorial.isFirstGame == 1)
        {
            IntroUI.SetActive(true);
            GameManager.Tutorial.isFirstGame = 2;
            PlayerPrefs.SetInt("isFirstGame", 2);
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && Time.timeScale ==1){
            Time.timeScale = 0;
            ExitUI.SetActive(true);
        }
    }
    public void ClickBack()
    {
        MenuUI.SetActive(false);
        DefaultUI.SetActive(true);
        Time.timeScale = 1;
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }

    public void Stage1()
    {
        Time.timeScale = 1;
        GameManager.isDie = false;
        GameManager.powerMode = false;
        GameManager.Stage1.GetCardKey = false;
        SceneManager.LoadScene("1st Floor");

    }

    public void Stage2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Papillon");
    }
    public void InputStage(string str)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(str);
    }
    public void EndGame()
    {
        Application.Quit();
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }
    public void ClickNo()
    {
        ExitUI.SetActive(false);
        DefaultUI.SetActive(true);       
        Time.timeScale = 1;
    }
    public void resetLife()
    {
        GameManager.totLife = 5;
    }

    public void saveData()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().SaveData();
    }
}
