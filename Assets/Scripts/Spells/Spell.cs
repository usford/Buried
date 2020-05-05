using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public string nameSpell = "";
    public float coolDown = 10.0f; //Время перезарядки способности
    public bool isActive = false;
    public Sprite icon;
    public TypeSpell typeSpell;
    [HideInInspector]
    public Player player;
    
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
