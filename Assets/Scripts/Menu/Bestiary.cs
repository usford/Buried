using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelInfo;
    public void GetInfo(string title)
    {
        panelInfo.GetComponent<PanelInfo>().title = title;
        panelInfo.SetActive(true);
    }
    public void Back()
    {
        gameObject.SetActive(false);
    }
}
