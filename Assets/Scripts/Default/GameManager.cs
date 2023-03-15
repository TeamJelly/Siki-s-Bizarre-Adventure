using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static bool isDie = false;
    public static bool isPause = false;
    public static bool powerMode = false;
    public static bool isGameOver = false;
    public static bool isGravityReverse = false;
    public static bool isNoPain = false;

    public static int clearState = 0;
    public static int totLife = 5;
    public static int totCoin = 0;

    public class Tutorial : MonoBehaviour
    {
        public static int isFirstGame = 0;
    }
    public class Stage1 : MonoBehaviour
    {
        public static bool GetCardKey = false;
        public static bool isClear = false;
        public static bool isPuzzleExit = false;
        public static bool Visited1st = false;
        public static bool Visited2nd = false;
        public static void SetVisitedFloor(string name)
        {
            if (name == "1st Floor")
                Visited1st = true;
            else if (name == "2nd Floor")
                Visited2nd = true;
        }
        public static bool CheckVisited(string name)
        {
            if (name == "1st Floor")
            {
                return Visited1st;
            }
            else if(name == "2nd Floor")
            {
                return Visited2nd;
            }
            return false;
        }
        public static void reset()
        {
            GetCardKey = false;
            isClear = false;
            Visited1st = false;
            Visited2nd = false;
        }
    }
    public class Stage2 : MonoBehaviour
    {
        public static bool isClaer = false;
    }
    public class Puzzle : MonoBehaviour
    {
        public static bool isCubeCatched = false;
        public static bool resetMode = false;
        public static int placedCube = 0;
        public static int currectCube = 0;
        public static void GoToStage()
        {
            totLife = 5;
            Time.timeScale = 1;
            Physics.gravity = new Vector3(0, -20, 0);
            playerMove8.jumpPower = Mathf.Abs(playerMove8.jumpPower);
            SceneManager.LoadScene("2nd Floor");
        }
    }
    void Start()
    {

        DontDestroyOnLoad(gameObject);
    }

    public void SaveData()
    {
        if (Stage1.isClear) clearState = 1;
        if (Stage2.isClaer) clearState = 2;
        PlayerPrefs.SetInt("Coin", totCoin);
        PlayerPrefs.SetInt("clearState", clearState);
    }

    public void LoadData()
    {
        Tutorial.isFirstGame = PlayerPrefs.GetInt("isFirstGame");
        totCoin = PlayerPrefs.GetInt("Coin");
        clearState = PlayerPrefs.GetInt("clearState");
    }
    public void resetData()
    {
        PlayerPrefs.SetInt("Coin", 0);
    }
    public void ClickMainMenu()
    {
        totLife = 5;
        Time.timeScale = 1;
        Stage1.reset();
        resetAll();
        SceneManager.LoadScene("Siki'sRoom");
    }
    public void InputStage(string str)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(str);
    }

    public static void resetAll()
    {
        isDie = false;
        isPause = false;
        powerMode = false;
        isGameOver = false;
        Physics.gravity = new Vector3(0, Mathf.Abs(Physics.gravity.y) * -1, 0);
        Time.timeScale = 1;
        totLife = 5;
    }

}
