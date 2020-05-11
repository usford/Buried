using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public string type; //Тип цели
    public int posX; //Позиция комнаты в массиве по X
    public int posY; //Позиция комнаты в массиве по Y
    private bool checkTarget = false;

    private GameManager gameManager;
    private float chanceSpawnChest = 0.2f; //Шанс заспавнить сундук
    private void Start() 
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update() 
    {
        if (!checkTarget)
        {
            if (type == "chest")
            {
                checkChest();
            }

            if (type == "enemies")
            {
                checkEnemies();
            }
        }   
    }

    //Нужно только открыть сундук
    private void checkChest()
    {
        if (GetComponent<Chest>().stateChest)
        {
            checkTarget = true;
            gameManager.boardScript.rooms[posX, posY].GetComponent<Room>().passed = true;
            targetCompleted();
        } 
    }

    //Нужно только убить нескольких врагов
    private void checkEnemies()
    {
        bool check = false;
        foreach (Transform child in gameManager.boardScript.currentRoom.GetComponentsInChildren<Transform>())
        {
            if(child.tag != "Enemy") continue;
            check = true;
        }

        if (!check)
        {
            checkTarget = true;
            gameManager.boardScript.rooms[posX, posY].GetComponent<Room>().passed = true;
            Prize();
            targetCompleted();
        }
    }

    //Награда за выполнение цели
    private void Prize()
    {
        int centreColumns = (int)Mathf.Floor(gameManager.boardScript.rooms[posX, posY].GetComponent<Room>().columns / 2);
        int centreRows = (int)Mathf.Floor(gameManager.boardScript.rooms[posX, posY].GetComponent<Room>().rows / 2);

        float random = Random.Range(0.0f, 1.1f);

        if (random <= chanceSpawnChest)
        {
            GameObject newChest = Instantiate(Resources.Load<GameObject>("Items/Chest1"), new Vector3(centreColumns, centreRows, 0.0f), Quaternion.identity);
            newChest.transform.SetParent(gameManager.boardScript.rooms[posX, posY].transform);
        }else
        {
            GameObject newGold = Instantiate(Resources.Load<GameObject>("Items/Gold1"), new Vector3(centreColumns, centreRows, 0.0f), Quaternion.identity);
            int randomGold = Random.Range(1, 16);
            newGold.GetComponent<Gold>().Amount += randomGold;
            newGold.transform.SetParent(gameManager.boardScript.rooms[posX, posY].transform);
        }
    }
    //Выполнение цели
    private void targetCompleted()
    {
        gameManager.playerStatistics.clearRooms += 1;
        gameManager.ui.ShowTextRoomCompleted(true);
        gameManager.boardScript.rooms[posX, posY].GetComponent<Room>().color = Color.green;
        gameManager.boardScript.ChangeExit(gameManager.boardScript.rooms[posX, posY].GetComponent<Room>().posX, gameManager.boardScript.rooms[posX, posY].GetComponent<Room>().posY);
    }
}
