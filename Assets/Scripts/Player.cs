using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Интерфейс персонажа
    public float maxSpeed = 2f; //Скорость
    public float maxHp = 3.0f; //Максимальное здоровье
    public float currentHp; //Текущее здоровье

    public float CurrentHp
    {
        get
        {
            return currentHp;
        }
        set
        {
            currentHp = value;
            ui.ChangeHealth(currentHp);
        }
    }
    public int amountGold = 0; //Количество золота у игрока

    public int AmountGold
    {
        get
        {
            return amountGold;
        }

        set
        {
            amountGold = value;
            ui.ChangeTextAmountGold(amountGold);
        }
    }
    public float invulnerability = 1.0f; //Временная неуязвимость после получения урона
    public bool invulnerabilityCheck = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private bool move = false;
    public GameObject spriteAttackSword;
    public float swordDamage = 20.0f;
    public float distanceAttackSword = 1.2f;
    public float powerForce = 15.0f; //Сила толчка 
    public float attackDelay = 1.0f;
    private bool attackCheck = true;
    private UI ui;
    public GameObject[] spells;

    public bool noDeath = false; //Невозможность умереть
    public GameObject rotateMoution; //Таргет для способности
    private void Awake()
    {
        currentHp = maxHp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        ui = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UI>();
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Horizontal")) Run("Horizontal");
        if (Input.GetButton("Vertical")) Run("Vertical");   
        if (Input.GetButtonDown("Fire1") && attackCheck) StartCoroutine(Attack());

        animator.SetBool("move", move);

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            move = false;
        }
    }

    private void Update() {
        RotateMoution();
    }

    private void RotateMoution()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);

        rotateMoution.transform.rotation = Quaternion.Euler(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);
    }


    //Передвижение персонажа
    private void Run(string type)
    {
        move = true;
        Vector3 direction = (type == "Horizontal")
            ? transform.right * Input.GetAxis("Horizontal")
            : transform.up * Input.GetAxis("Vertical");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * maxSpeed);

        

        if (type == "Horizontal")
        {
            spriteRenderer.flipX = (direction.x < 0.0f) ? true : false;
            
        }
    }

    private IEnumerator Attack()
    {
        attackCheck = false;

        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);

        Vector2 movement = mousePosition - transform.position;
        movement.Normalize();

        //Попал ли по врагу
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, movement, distanceAttackSword);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Enemy>().ReceiveDamage(swordDamage);
                hit.collider.GetComponent<Rigidbody2D>().AddForce(movement * powerForce, ForceMode2D.Impulse);
            }
        }

        GameObject attack = Instantiate(spriteAttackSword, transform.position, Quaternion.Euler(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle));
        yield return new WaitForSeconds(0.4f);
        Destroy(attack);    

        float count = 0;
        while (count < attackDelay)
        {
            yield return new WaitForSeconds(0.1f);
            count += 0.1f;
        }

        attackCheck = true;
    }

    //Полученный урон
    public void ReceiveDamage(float damageTaken)
    {
        if (invulnerabilityCheck) return;
        invulnerabilityCheck = true;
        CurrentHp -= damageTaken;
        StartCoroutine(DamageAnimation());  
        if (CurrentHp <= 0 && !noDeath)
        {
            Death();
            
        }
    }

    //Анимация получения урона
    private IEnumerator DamageAnimation()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.3f);

        float count = 0.0f;

        while(count < invulnerability)
        {
                
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().color = Color.black;
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().color = Color.white;
            count += 0.4f;
        }

        invulnerabilityCheck = false;
    }

    private void Death()
    {
        ui.ShowTextDeath();
        Destroy(gameObject);
    }
}
