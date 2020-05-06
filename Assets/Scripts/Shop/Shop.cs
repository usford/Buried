using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameObject player;
    public GameObject text;
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

            if (Input.GetKeyDown(KeyCode.E))
            {
                gameManager.ui.shopPanel.SetActive(true);
            }
        }else
        {
            text.SetActive(false);
        }
    }
}
