using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [HideInInspector]
    public bool isActive = false;
    [HideInInspector]
    public TypeSpell typeSpell;
    [HideInInspector]
    public Player player;
    public SpellInfo spellInfo; //Глобальная информация о способности
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
