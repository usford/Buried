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
    public PlayerStatistics playerStatistics;
    public GameInfo gameInfo;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
        ui = GetComponent<UI>();
        InitGame(gameInfo.currentLevel);
    }

    private void Update() 
    {
        #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(playerStatistics); 
        #endif
        //Рестарт игры
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Переход на следующий уровень
        if (Input.GetKeyDown(KeyCode.T))
        {
            gameInfo.currentLevel += 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

        //Сброс всех найденных способностей/бафов
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            allSpells.ForEach((spell) =>
            {
                spell.isFound = false;
                #if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(spell); 
                #endif
            });

            allBuffs.ForEach((buff) =>
            {
                buff.isFound = false;
                #if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(buff); 
                #endif
            });
        }

        //Открытие главного меню
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ui.menuPanel.SetActive(!ui.menuPanel.activeSelf);
        }
    }

    public void InitGame(int level)
    {
        boardScript.SetupScene(level);
        ui.GetMiniMap(boardScript.rooms);
    }
}
