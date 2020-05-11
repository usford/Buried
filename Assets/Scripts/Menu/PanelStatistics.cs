using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelStatistics : MonoBehaviour
{
    public InfoHelper infoHelper;
    public Text textClearRooms;
    public Text textOpenChests;
    public Text textGoldCollected;
    public Text textDiedEnemy;

    private void Start() 
    {
        textClearRooms.text += infoHelper.playerStatistics.clearRooms.ToString();
        textOpenChests.text += infoHelper.playerStatistics.openChests.ToString();
        textGoldCollected.text += infoHelper.playerStatistics.goldCollected.ToString();
        textDiedEnemy.text += infoHelper.playerStatistics.diedEnemy.ToString();
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }
}
