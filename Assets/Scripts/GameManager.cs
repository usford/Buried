﻿using System.Collections;
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

        //DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
        ui = GetComponent<UI>();
        InitGame();
    }

    private void Update() 
    {
        //Рестарт игры
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Проверка открытия комнаты
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!boardScript.currentRoom.GetComponent<Room>().passed)
            {
                ui.ShowTextRoomCompleted();
                boardScript.currentRoom.GetComponent<Room>().passed = true;
                boardScript.currentRoom.GetComponent<Room>().color = Color.green;
                boardScript.ChangeExit(boardScript.currentRoom.GetComponent<Room>().posX, boardScript.currentRoom.GetComponent<Room>().posY);
            }
        }
    }

    void InitGame()
    {
        boardScript.SetupScene(level);
        ui.GetMiniMap(boardScript.rooms);
    }
}
