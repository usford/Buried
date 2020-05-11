using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private GameObject player;

    public GameObject text;
    public Sprite openChest;
    public bool stateChest = false;
    private GameManager gameManager;
    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update() 
    {
        if ((Vector3.Distance(transform.position, player.transform.position) <= 1.0f))
        {
            text.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && !stateChest)
            {
                GetComponent<Animator>().enabled = false;
                text.GetComponent<Text>().text = "";
                GetComponent<SpriteRenderer>().sprite = openChest;
                stateChest = true;
                gameManager.playerStatistics.openChests += 1;
            }
        }else
        {
            text.SetActive(false);
        }
    }
}
