using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScript;
    public static GameManager instance = null;
    public UI ui;
    

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
        //Рестарт игры
        if (Input.GetKeyDown(KeyCode.R))
        {
            InitGame();
        }

        //Проверка открытия комнаты
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!boardScript.currentRoom.passed)
            {
                ui.ShowTextRoomCompleted();
                boardScript.currentRoom.passed = true;
                boardScript.changeExit(boardScript.currentRoom.posX, boardScript.currentRoom.posY);
            }
            //boardScript.MapRendering(boardScript.currentRoom.posX, boardScript.currentRoom.posY);
        }
    }

    void InitGame()
    {
        boardScript.SetupScene(level);
        ui.GetMiniMap(boardScript.map);
    }
}
