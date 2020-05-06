using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [HideInInspector]
    public string nameSpell;
    [HideInInspector]
    public float coolDown; //Время перезарядки способности
    [HideInInspector]
    public bool isActive = false;
    [HideInInspector]
    public Sprite icon;
    public TypeSpell typeSpell;
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public string description; //Описание
    [HideInInspector]
    public int lvl; //Уровень способности
    [HideInInspector]
    public bool isFound = false; //Найдена ли способность
    public SpellInfo spellInfo; //Глобальная информация о способности

    private void Awake() 
    {
        nameSpell = spellInfo.nameSpell;
        coolDown = spellInfo.coolDown;
        icon = spellInfo.icon;
        lvl = spellInfo.lvl;
        isFound = spellInfo.isFound;
        description = spellInfo.description;

    }
    public virtual void ActivateSpell()
    {

    }
    

    //Типы способностей
    public enum TypeSpell
    {
        Comet,
        Shield
    }
}
