using System.Collections;
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

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentHp = maxHp;
    }

    //Урон, полученный врагом
    public void Damage(float damageTaken)
    {
        // Debug.Log(currentHp);
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
        Vector2 movement = player.GetComponent<Transform>().position - transform.position ;
        player.GetComponent<Rigidbody2D>().AddForce(movement * powerForce, ForceMode2D.Impulse);
        player.Damage(damage); 
    }

    //Смерть
    public virtual void Death()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Hit();
        }
    }
}
