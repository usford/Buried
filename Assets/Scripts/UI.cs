using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public Transform[,] miniMap = new Transform[30, 30];

    private void Start()
    {
        //GetMiniMap();
    }

    public void GetMiniMap(BoardManager.Map[,] map)
    {
        int countY = 0;

        Transform rows = GameObject.Find("Mini_map").GetComponent<Transform>();

        foreach (Transform row in rows)
        {
            countY++;
        }
        countY--;

        int mmX = 0;
        int mmY = countY;
        foreach (Transform row in rows)
        {
            mmX = 0;
            foreach (Transform tale in row)
            {
                tale.gameObject.SetActive(false);
                miniMap[mmX, mmY] = tale;
                mmX++;
            }
            //Debug.Log($"Y: {y}");
            mmY--;
        }

        Debug.Log(map);

        int count = 0;
        foreach (BoardManager.Map m in map)
        {
            if (m == null) continue;
            count++;
        }

        Debug.Log("Кол-во комнат: " + count);

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] != null && miniMap[x, y] != null) miniMap[x,y].gameObject.SetActive(true);;
            }
        }
    }
}
