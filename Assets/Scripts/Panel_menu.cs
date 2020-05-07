using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel_menu : MonoBehaviour
{
    public void ContinueGame()
    {
        gameObject.SetActive(false);
    }
    public void Exit()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
