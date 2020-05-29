using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private GameObject player;
    private GameManager gameManager;

    public GameObject text;

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
                gameManager.gameInfo.currentLevel = (gameManager.gameInfo.currentLevel + 1 > 2) ? 1 : gameManager.gameInfo.currentLevel + 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }else
        {
            text.SetActive(false);
        }
    }
}
