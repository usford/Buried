using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public int roomX = 1;
    public int roomY = 1;

    private GameManager gameManager;
    private BoardManager boardScript;

    private UI ui;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        boardScript = gameManager.boardScript;
        ui = gameManager.ui;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && boardScript.currentRoom.GetComponent<Room>().passed)
        {
            // Debug.Log(boardScript.currentRoom.GetComponent<Room>().posX + "::::::::::::" + boardScript.currentRoom.GetComponent<Room>().posY);
            // Debug.Log(roomX + "!!!!!!" + roomY);

            int spawnedX = 0;
            int spawnedY = 0;
            var dir = "";

            if (boardScript.currentRoom.GetComponent<Room>().posX - roomX == 1)
            {
                dir = "Слева зашёл";
            }else if (boardScript.currentRoom.GetComponent<Room>().posX - roomX == -1)
            {
                dir = "Справа зашёл";
            }else if (boardScript.currentRoom.GetComponent<Room>().posY - roomY == 1)
            {
                dir = "Снизу зашёл";
            }else if (boardScript.currentRoom.GetComponent<Room>().posY - roomY == -1)
            {
                dir = "Сверху зашёл";
            }

            if (dir == "Слева зашёл")
            {
                spawnedX = boardScript.rooms[roomX, roomY].GetComponent<Room>().columns - 2;
                spawnedY = (int)Mathf.Floor(boardScript.rooms[roomX, roomY].GetComponent<Room>().rows / 2);
            }else if (dir == "Справа зашёл")
            {
                spawnedX = 1;
                spawnedY = (int)Mathf.Floor(boardScript.rooms[roomX, roomY].GetComponent<Room>().rows / 2);
            }else if (dir == "Сверху зашёл")
            {
                spawnedX = (int)Mathf.Floor(boardScript.rooms[roomX, roomY].GetComponent<Room>().columns / 2);
                spawnedY = 0;
            }else if (dir == "Снизу зашёл")
            {
                spawnedX = (int)Mathf.Floor(boardScript.rooms[roomX, roomY].GetComponent<Room>().columns / 2);
                spawnedY = boardScript.rooms[roomX, roomY].GetComponent<Room>().rows - 2;
            }

            ui.PaintingRoom(boardScript.rooms, roomX, roomY, boardScript.currentRoom.GetComponent<Room>().posX, boardScript.currentRoom.GetComponent<Room>().posY);
            boardScript.RoomRendering(roomX, roomY, spawnedX, spawnedY);
        }
    }
}
