using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScript;
    public static GameManager instance = null;
    private UI ui;
    

    private int level = 1;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
        ui = GetComponent<UI>();
        InitGame();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            InitGame();
        }
    }

    void InitGame()
    {
        boardScript.SetupScene(level);
        ui.GetMiniMap(boardScript.map);
    }
}
