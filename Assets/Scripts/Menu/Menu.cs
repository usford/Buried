using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject btnContinueGame;
    public GameObject panelBestiary;
    private bool isGameStarted = false;
    private void Start() 
    {
        Color color = btnContinueGame.GetComponent<Image>().color;
        color.a = (isGameStarted) ? 1 : 0.3f;
        btnContinueGame.GetComponent<Image>().color = color;
    }
    public void ContinueGame()
    {

    }

    public void NewGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void Bestiary()
    {
        panelBestiary.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
