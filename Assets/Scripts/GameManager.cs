using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScript;
    public static GameManager instance = null;
    public UI ui;

    public List<SpellInfo> allSpells;
    public List<BuffInfo> allBuffs;
    

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
            // if (!boardScript.currentRoom.GetComponent<Room>().passed)
            // {
            //     ui.ShowTextRoomCompleted();
            //     boardScript.currentRoom.GetComponent<Room>().passed = true;
            //     boardScript.currentRoom.GetComponent<Room>().color = Color.green;
            //     boardScript.ChangeExit(boardScript.currentRoom.GetComponent<Room>().posX, boardScript.currentRoom.GetComponent<Room>().posY);
            // }
        }

        //Сброс всех улучшений
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            allSpells.ForEach((spell) =>
            {
                spell.lvl = 0;
            });

            allBuffs.ForEach((buff) =>
            {
                buff.lvl = 0;
            });
        }
    }

    void InitGame()
    {
        boardScript.SetupScene(level);
        ui.GetMiniMap(boardScript.rooms);
    }
}
