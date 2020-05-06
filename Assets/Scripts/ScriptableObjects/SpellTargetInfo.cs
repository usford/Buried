using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SpellTargetInfo", menuName = "Buried/SpellTargetInfo", order = 0)]
public class SpellTargetInfo : ScriptableObject 
{
    public float damage; //Урон от способности
    public float spellSpeed; //Скорость полёта способности
}

