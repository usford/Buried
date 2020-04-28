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
            ui.PaintingRoom(boardScript.rooms, roomX, roomY, boardScript.currentRoom.GetComponent<Room>().posX, boardScript.currentRoom.GetComponent<Room>().posY);
            boardScript.RoomRendering(roomX, roomY);
        }
    }
}
