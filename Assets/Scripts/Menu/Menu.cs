using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject btnContinueGame;
    public GameObject panelBestiary;
    public GameObject panelStatistics;
    private bool isGameStarted = false;
    public GameInfo gameInfo;
    private void Start() 
    {
        Color color = btnContinueGame.GetComponent<Image>().color;
        if (gameInfo.currentLevel > 1) isGameStarted = true;
        color.a = (isGameStarted) ? 1 : 0.3f;
        btnContinueGame.GetComponent<Image>().color = color;
    }
    public void ContinueGame()
    {
        if (isGameStarted) SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void NewGame()
    {
        gameInfo.currentLevel = 1;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void Bestiary()
    {
        panelBestiary.SetActive(true);
    }

    public void Statistics()
    {
        panelStatistics.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
