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
    public List<GameObject> enemies; //Враги, которые могут заспавнится в этой комнате
    public bool spawnChest = false; //Может ли в этой комнате заспавниться сундук
    public float chanceSpawnChest = 0.2f;
    public int maxEnemy = 2; //Максимальное количество врагов в комнате
    public int minEnemy = 1; //Минимальное количество врагов в комнате
    public List<Vector2> spawnedPositions; //Где может что-либо заспавниться

    private void Start()
    {
        // Transform children = GetComponent<Transform>();

        // foreach (Transform child in children)
        // {
        //     Debug.Log(child);
        // }
    }

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
