using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float maxHp = 100.0f; //Максимальное здоровье
    [HideInInspector]
    public float currentHp; //Текущее здоровье
    [HideInInspector]
    public float damage = 20.0f; //Урон
    public float powerForce = 10.0f; //Сила толчка
    public float maxSpeed = 2.0f; //Максимальная скорость врага
    public float currentSpeed = 2.0f; //Текущая скорость врага
    [HideInInspector]
    public Player player;
    public int maxDropGold = 1; //Максимальное кол-во золота, которое может выпасть с моба
    public int minDropGold = 0; // Минимальное кол-во золота, которое может выпасть с моба
    public float chanceDropGold = 0.5f; //Шанс выпадения золота с монстра
    [HideInInspector]
    public GameManager gameManager;
    private Text textDamage; //Текст урона
    public bool dropGold = true;

    public EnemyInfo enemyInfo;

    private void Start() 
    {
        maxHp = enemyInfo.hp;
        damage = enemyInfo.damage;
        currentHp = maxHp;
        currentSpeed = maxSpeed;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        textDamage = GetComponentInChildren<Text>();
    }

    //Урон, полученный врагом
    public void ReceiveDamage(float damageTaken)
    {
        float plusDamage = 0.0f;

        //plusDamage = player.CheckBuff(Buff.UniqueNameBuff.Damage, damageTaken, Buff.TypeBuff.Numeric);

        bool check = player.CheckBuff(Buff.UniqueNameBuff.Damage);

        if (check)
        {
            plusDamage = player.ActivateNumericBuff(Buff.UniqueNameBuff.Damage, damageTaken);
        }   
        damageTaken += plusDamage;
        currentHp -= damageTaken;
        StartCoroutine(TextDamageAnimation(damageTaken));
        
        StartCoroutine(DamageAnimation());  
    }

    private IEnumerator TextDamageAnimation(float damageTaken)
    {
        textDamage.text = $"-{damageTaken}";
        Color color = textDamage.color;
        color.a = 1;

        textDamage.color = color;
        float count = 0.0f;
        textDamage.transform.position = transform.position;
        while(count < 0.3f)
        {
            textDamage.transform.Translate(transform.up * 0.03f);
            count += 0.03f;
            yield return new WaitForSeconds(0.03f);
        }
        //yield return new WaitForSeconds(1f);
        color.a = 0;
        textDamage.color = color;
    }

    //Анимация получения урона
    private IEnumerator DamageAnimation()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        if (currentHp <= 0)
        {
            Death();
        }else
        {    
            bool check = player.CheckBuff(Buff.UniqueNameBuff.Freezing);    
            if (check)
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
                player.ActivateTargetBuff(Buff.UniqueNameBuff.Freezing, gameObject);
            }else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }  
    }

    //Монстр наносит удар
    public void Hit()
    {   
        Vector2 movement = player.GetComponent<Transform>().position - transform.position ;
        player.GetComponent<Rigidbody2D>().AddForce(movement * powerForce, ForceMode2D.Impulse);
        player.ReceiveDamage(damage); 
    }

    //Смерть
    public virtual void Death()
    {
        float random = Random.Range(0.0f, chanceDropGold + 0.1f);

        if (random <= chanceDropGold && dropGold)
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
