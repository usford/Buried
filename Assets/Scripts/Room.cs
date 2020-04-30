using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int columns; //Высота в блоках
    public int rows; //Ширина в блоках
    public int spawnedX;
    public int spawnedY;
    public Color color; //Цвет комнаты
    public int posX;
    public int posY;
    public bool passed = false;

    private void OnDisable() 
    { 
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            if (child.tag == "Trash")
            {
                Destroy(child.gameObject);
            }
        }

        GameObject[] trash = GameObject.FindGameObjectsWithTag("Trash");
        
        foreach (GameObject t in trash)
        {
            Destroy(t);
        }
    }
}
