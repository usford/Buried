﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHp = 100.0f; //Максимальное здоровье
    public float currentHp; //Текущее здоровье
    public float damage = 20.0f; //Урон
    public float powerForce = 10.0f; //Сила толчка
    public float maxSpeed = 2.0f; //Скорость врага
    public Player player;
    public int maxDropGold = 1; //Максимальное кол-во золота, которое может выпасть с моба
    public int minDropGold = 0; // Минимальное кол-во золота, которое может выпасть с моба
    public float chanceDropGold = 0.5f; //Шанс выпадения золота с монстра
    public GameManager gameManager;

    private void Start() 
    {
        currentHp = maxHp;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    //Урон, полученный врагом
    public void ReceiveDamage(float damageTaken)
    {
        // Debug.Log(damageTaken);
        currentHp -= damageTaken;
        StartCoroutine(DamageAnimation());  
    }

    //Анимация получения урона
    private IEnumerator DamageAnimation()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        if (currentHp <= 0)
        {
            Death();
        }
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    //Монстр наносит удар
    private void Hit()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Vector2 movement = player.GetComponent<Transform>().position - transform.position ;
        player.GetComponent<Rigidbody2D>().AddForce(movement * powerForce, ForceMode2D.Impulse);
        player.ReceiveDamage(damage); 
    }

    //Смерть
    public virtual void Death()
    {
        float random = Random.Range(0.0f, chanceDropGold + 0.1f);

        if (random <= chanceDropGold)
            DropGold();

        Destroy(gameObject);
    }

    //Дроп золота при смерти монстра
    public virtual void DropGold()
    {
        int randomGold = Random.Range(minDropGold, maxDropGold + 1);

        GameObject newGold = Instantiate(Resources.Load<GameObject>("Items/Gold1"), transform.position, Quaternion.identity);
        newGold.GetComponent<Gold>().Amount += randomGold;
        newGold.transform.SetParent(gameManager.boardScript.currentRoom.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Hit();
        }
    }
}
