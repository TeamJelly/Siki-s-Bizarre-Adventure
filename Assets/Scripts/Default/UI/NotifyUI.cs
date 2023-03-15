using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyUI : MonoBehaviour {

    public GameObject DefaultUI;
    public bool EnterType;
    public bool TimeType;
    public float times = 1.5f;

    void OnEnable()
    {
        if (EnterType) StartCoroutine(EnterT());
        else if (TimeType) StartCoroutine(TimeT());
    }
    IEnumerator EnterT()
    {

        while (!Input.GetButtonDown("Submit")) yield return null;
        //yield return new WaitForSeconds(3f);
        DefaultUI.SetActive(true);
        this.gameObject.SetActive(false);
        
    }

    IEnumerator TimeT()
    {
        yield return new WaitForSeconds(times);
        DefaultUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
