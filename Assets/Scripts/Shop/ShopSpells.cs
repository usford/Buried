﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpells : MonoBehaviour
{
    public GameObject shopPanel;
    public void Exit()
    {
        gameObject.SetActive(false);
        shopPanel.SetActive(true);
    }
}
