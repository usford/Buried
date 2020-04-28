using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Transform[,] miniMap = new Transform[30, 30];
    private Text textRoomCompleted;

    private void Start()
    {
        //GetMiniMap();
        textRoomCompleted = GameObject.Find("Text_Room_Completed").GetComponent<Text>();  
    }

    //Пока текста о том, что комната зачищена
    public void ShowTextRoomCompleted()
    {     
        StartCoroutine(SmoothAppearance(textRoomCompleted));   
    }

    //Плавное появление надписи
    private IEnumerator SmoothAppearance(Text text)
    {
        Color color = textRoomCompleted.color;
        color.a = 0.0f;

        while(color.a < 1.0f)
        {
            yield return new WaitForSeconds(0.05f);

            color.a += 0.05f;
            textRoomCompleted.color = color;
        }

        while(color.a > 0.0f)
        {
            yield return new WaitForSeconds(0.05f);

            color.a -= 0.05f;
            textRoomCompleted.color = color;
        }
    }

    public void GetMiniMap(GameObject[,] rooms)
    {
        PaintingAllRooms();
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

        //Debug.Log(map);

        int count = 0;
        foreach (GameObject m in rooms)
        {
            if (m == null) continue;
            count++;
        }

        //Debug.Log("Кол-во комнат: " + count);

        for (int x = 0; x < rooms.GetLength(0); x++)
        {
            for (int y = 0; y < rooms.GetLength(1); y++)
            {
                if (rooms[x, y] != null && miniMap[x, y] != null)
                {
                    miniMap[x, y].gameObject.GetComponent<Image>().color = rooms[x,y].GetComponent<Room>().color;
                    miniMap[x,y].gameObject.SetActive(true);
                }
            }
        }

        miniMap[5,5].GetComponent<Image>().color = Color.white;
    }

    //Закрашивание белым цветов текущую комнату
    public void PaintingRoom(GameObject[,] rooms, int currentPosX, int currentPosY, int pastPosX, int pastPosY)
    {
        miniMap[pastPosX,pastPosY].GetComponent<Image>().color = rooms[pastPosX,pastPosY].GetComponent<Room>().color;
        miniMap[currentPosX,currentPosY].GetComponent<Image>().color = Color.white;
    }

    private void PaintingAllRooms()
    {
        foreach (Transform m in miniMap)
        {
            if (m == null) continue;
            m.GetComponent<Image>().color = Color.red;
        }
    }
}
