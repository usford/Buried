using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffFreezingDetails", menuName = "Buried/Buffs/BuffFreezingDetails", order = 0)]
public class BuffFreezingDetails : ScriptableObject 
{
    public float freezeTime; //Сколько персонаж будет заморожен
    public float freezeSpeed; //На сколько персонаж будет медленее двигаться
}

