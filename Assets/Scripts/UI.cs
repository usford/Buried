using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Transform[,] miniMap = new Transform[30, 30];
    private Text textRoomCompleted; //Текст о зачистки комнаты
    private Text textAmountGold; //Текст о количестве золота
    private GameObject health; //Здоровье персонажа (Список сердец)
    private Text textDeath; //Текст о смерти героя
    private Player player;
    private GameObject spells; //Способности персонажа
    private GameObject videoDeath; //Видео-смерть игрока

    private void Start()
    {
        //GetMiniMap();
        textRoomCompleted = GameObject.Find("Text_room_completed").GetComponent<Text>(); 
        textAmountGold = GameObject.Find("Text_amount_gold").GetComponent<Text>();
        health = GameObject.Find("Health");
        textDeath = GameObject.Find("Text_death_player").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spells = GameObject.Find("Spells");
        videoDeath = GameObject.Find("Video_death");

        //Начальные значения
        ChangeHealth(player.CurrentHp);
        ChangeSpells(player.spells);
    }

    //Изменение способностей
    public void ChangeSpells(List<GameObject> playerSpells)
    {
        Transform frames = spells.GetComponent<Transform>();

        for (int i = 0; i < playerSpells.Count; i++)
        {
            var spell = frames.GetChild(i).GetChild(0);
            spell.GetComponent<SpriteRenderer>().sprite = playerSpells[i].GetComponent<Spell>().icon;
            spell.GetComponent<SpellIcons>().nameSpell = playerSpells[i].name;
            spell.GetComponent<SpellIcons>().coolDown = playerSpells[i].GetComponent<Spell>().coolDown;
        }
    }

    //Сообщение о том, что игрок мёртв
    public void ShowTextDeath()
    {
        StartCoroutine(SmoothAppearance(textDeath, false));  
        //StartCoroutine(videoDeath.GetComponent<DeathVideo>().PlayVideo());
    }

    //Изменение хп героя
    public void ChangeHealth(float hp)
    {
        Transform[] children = health.transform.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child.name == "Health") continue;
            Destroy(child.gameObject);
        }

        for (int i = 0; i < hp; i++)
        {
            GameObject heart = Instantiate(Resources.Load<GameObject>("UI/Heart"));
            heart.transform.SetParent(health.transform);

            Vector3 vec3 = new Vector3(0.5f, 0.7f);
            heart.transform.localScale = vec3;
        }
    }
    //Изменение текста с количеством золота
    public void ChangeTextAmountGold(int amountGold)
    {
        textAmountGold.text = amountGold.ToString();
    }

    //Пока текста о том, что комната зачищена
    public void ShowTextRoomCompleted(bool hideText)
    {     
        StartCoroutine(SmoothAppearance(textRoomCompleted, hideText));  
    }

    //Плавное появление надписи
    private IEnumerator SmoothAppearance(Text text, bool hideText)
    {
        Color color = text.color;
        color.a = 0.0f;

        while(color.a < 1.0f)
        {
            yield return new WaitForSeconds(0.05f);

            color.a += 0.05f;
            text.color = color;
        }

        if (!hideText) yield break;

        while(color.a > 0.0f)
        {
            yield return new WaitForSeconds(0.05f);

            color.a -= 0.05f;
            text.color = color;
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
