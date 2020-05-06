using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BuffInfo", menuName = "Buried/BuffInfo", order = 0)]
public class BuffInfo : ScriptableObject 
{
    public string nameBuff; //Название бафа
    public float duration; //Время действия бафа
    public string description; //Описание бафа
    public GameObject imageTimer; //Таймер бафа
    public bool isEndless = false; //Бесконечный баф
    public ScriptableObject buff; //Баф
    public int lvl; //Лвл бафа
    public bool isFound; //Найден ли баф
    public int priceLvl; //Цена за улучшение бафа
    public int maxLvl; //Максимальное количество улучшений
    public Sprite icon;
}
