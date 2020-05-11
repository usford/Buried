using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatistics", menuName = "Buried/PlayerStatistics", order = 0)]
public class PlayerStatistics : ScriptableObject
{
    public int clearRooms; //Сколько защищено комнат
    public int openChests; //Сколько открыто сундуков
    public int goldCollected; //Собрано золота
    public int diedEnemy; //Сколько умерло врагов
}
