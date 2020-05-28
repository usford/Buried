using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameInfo", menuName = "Buried/GameInfo", order = 0)]
public class GameInfo : ScriptableObject 
{
    public int currentLevel; //Текущий уровень
}

