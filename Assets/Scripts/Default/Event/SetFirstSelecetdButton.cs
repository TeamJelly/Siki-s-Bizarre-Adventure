using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SetFirstSelecetdButton : MonoBehaviour {

    public GameObject FirstSelected;
    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(FirstSelected);
    }
}
