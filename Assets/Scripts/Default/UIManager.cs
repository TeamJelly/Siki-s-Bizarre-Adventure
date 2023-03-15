using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour {


     private GameObject pauseUI;
     private GameObject playUI;
     private GameObject popUI;
     private GameObject GameOverUI;
     public static Text txtLife;
     public static Text txtCoin;

    void Start()
    {
        Time.timeScale = 1;

        txtLife = GameObject.Find("txtLife").GetComponent<Text>();
        txtCoin = GameObject.Find("txtCoin").GetComponent<Text>();
        pauseUI = GameObject.Find("pauseUI");
        playUI = GameObject.Find("playUI");
        popUI = GameObject.Find("popUI");
        GameOverUI = GameObject.Find("GameOverUI");


        DispLife(0);
        DispCoin(0);

        pauseUI.SetActive(false);
        popUI.SetActive(false);
        GameOverUI.SetActive(false);
        playUI.SetActive(true);
    }

    void Update()
    {

        if (Input.GetButtonDown("Cancel")&& Time.timeScale > 0)
        {
            TogglePauseMenu();
        }
    }
    public void gameOver()
    {
        GameManager.totLife = 5;
        playUI.SetActive(false);
        GameOverUI.SetActive(true);
        Time.timeScale = 0;

    }

    public IEnumerator popUp()
    {
        if(GameObject.Find("popUI(Clone)")) Destroy(GameObject.Find("popUI(Clone)"));

        GameObject popCopy = GameObject.Instantiate(popUI);
        popCopy.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
        popCopy.transform.position = popUI.transform.position;
        Text poptxt = popCopy.GetComponentInChildren<Text>();
        if (GameManager.isDie) poptxt.text = "죽었다.";
        else if (GameManager.powerMode) poptxt.text = "파워모드";
        else if(!GameManager.powerMode) poptxt.text = "파워모드 해제";
        popCopy.SetActive(true);
        yield return new WaitForSeconds(2);
        Destroy(popCopy);
    }



   //************************버튼에 사용되는 함수들****************************
    public void TogglePauseMenu()
    {
        if(Time.timeScale == 1)
        {
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
            playUI.SetActive(false);
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseUI.SetActive(false);
            playUI.SetActive(true);
            Time.timeScale = 1;
        }
    }
    public static void DispLife(int life)
    {
        //if (life < 0) NotiUI.SetActive(true);
        GameManager.totLife += life;
        txtLife.text = "X" + GameManager.totLife.ToString();
    }

    public static  void DispCoin(int coin)
    {
        GameManager.totCoin += coin;
        txtCoin.text = "X" + GameManager.totCoin.ToString();
    }

    public void ClickResume()
    {
        pauseUI.SetActive(false);
        playUI.SetActive(true);
        Time.timeScale = 1;
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }

    public void ClickOption()
    {
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }

    public void ClickMainMenu()
    {
        GameManager.resetAll();
        SceneManager.LoadScene("Siki'sRoom");

    }

    public void EndGame()
    {
        Application.Quit();
    }
    public void ClickRestart()
    {
        GameManager.resetAll();
        GameManager.Stage1.reset();
        GameObject.Find("GameManager").GetComponent<GameManager>().LoadData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ClickPauseMenu(GameObject thisUI)
    {
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        pauseUI.SetActive(true);
        thisUI.SetActive(false);
    }

    public void SetStartButton(GameObject startButton)
    {
        EventSystem.current.SetSelectedGameObject(startButton);
    }



}
