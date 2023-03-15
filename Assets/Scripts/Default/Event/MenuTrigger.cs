using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTrigger : MonoBehaviour {

   
    public GameObject StatusUI;
    public Text text;
    public string Contents;
    private GameObject DefaultUI;
    public bool StopUI;
    public bool useOriginTxt;
    public bool AutoRemove;

    private bool isDelayTime = false;

    void Start()
    {
        DefaultUI = GameObject.Find("playUI");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player") && !isDelayTime)
        {
            if(!useOriginTxt) text.text = Contents;
            DefaultUI.SetActive(false);
            StatusUI.SetActive(true);
            if (StopUI) Time.timeScale = 0;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            DefaultUI.SetActive(true);
            StatusUI.SetActive(false);
            if (StopUI) StartCoroutine(ExitDelay());
        }
    }

    IEnumerator ExitDelay()
    {
        isDelayTime = true;
        yield return new WaitForSeconds(0.5f);
        isDelayTime = false;
    }
 }
