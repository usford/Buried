using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellInfo", menuName = "Buried/SpellInfo", order = 0)]
public class SpellInfo : ScriptableObject 
{
    public string nameSpell;
    public float coolDown; //Время перезарядки способности
    public string description;
    public string descriptionUpgrade; //Описание для апа способности
    public Sprite icon;
    public int lvl;
    public bool isFound;
    public ScriptableObject spell;
    public int priceLvl; //Цена за улучшение уровня
    public int maxLvl; //Максимальное количество улучшений
}