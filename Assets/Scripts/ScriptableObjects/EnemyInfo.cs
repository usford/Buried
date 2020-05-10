using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Buried/EnemyInfo", order = 0)]
public class EnemyInfo : ScriptableObject 
{
    public string nameEnemy; //Название врага
    public string description; //Описание врага
    public Sprite icon; //Картинка враг
    public float damage; //Урон врага
    public float hp; //Хп врага
}
