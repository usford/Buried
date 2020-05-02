using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Player player;
    private GameManager gameManager;
    private string isRoomBig = ""; //Является ли комната большой, чтобы включить слежение камеры, и с какой стороны она большая
    public int maxColumns = 25; //Сколько блоков в ширину нужно, чтобы включить слежение камеры
    public int maxRows = 15; //Сколько блоков в высоту нужно, чтобы включить слежение камеры

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update() 
    {
        CheckRoom();    
        if (player != null)
        {
            switch(isRoomBig)
            {
                case "everywhere":
                {
                    break;
                }

                case "columns":
                {
                    transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                    break;
                }

                case "rows":
                {
                    transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
                    break;
                }
                default: break;
            }
        }
    }

    //Является ли комната большой
    private void CheckRoom()
    {
        if (gameManager.boardScript.currentRoom.GetComponent<Room>().columns == maxColumns && gameManager.boardScript.currentRoom.GetComponent<Room>().rows == maxRows)
        {
            isRoomBig = "everywhere";
        }
        else if (gameManager.boardScript.currentRoom.GetComponent<Room>().columns >= maxColumns)
        {
            isRoomBig = "columns";
        }else if (gameManager.boardScript.currentRoom.GetComponent<Room>().rows >= maxRows)
        {
            isRoomBig = "rows";
        }else
        {
            isRoomBig = "";
        }
    }
}
