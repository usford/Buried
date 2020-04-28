using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHp = 100.0f; //Максимальное здоровье
    public float currentHp; //Текущее здоровье
    public float damage = 20.0f; //Урон

    private void Awake() 
    {
        currentHp = maxHp;
    }

    //Урон, полученный врагом
    public void Damage(float damageTaken)
    {
        currentHp -= damageTaken;
        StartCoroutine(DamageAnimation());  
        if (currentHp <= 0)
        {
            Death();
        }
    }

    //Анимация получения урона
    private IEnumerator DamageAnimation()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    //Смерть
    private void Death()
    {
        Destroy(gameObject);
    }
}
