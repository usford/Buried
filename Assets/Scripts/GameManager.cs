using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void InitGame()
    {
        boardScript.SetupScene(level);
        ui.GetMiniMap(boardScript.map);
    }
}
