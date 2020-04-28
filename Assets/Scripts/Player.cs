using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Интерфейс персонажа
    public float maxSpeed = 2f; //Скорость
    public float maxHp = 100.0f; //Максимальное здоровье
    public float currentHp; //Текущее здоровье
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private bool move = false;
    public GameObject spriteAttackSword;
    public float swordDamage = 20.0f;
    public float distanceAttackSword = 1.2f;
    public float powerForce = 15.0f; //Сила отталкивания 
    public float attackDelay = 1.0f;
    private bool attackCheck = true;

    private void Awake()
    {
        currentHp = maxHp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);;
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);

        Vector2 movement = mousePosition - transform.position;
        movement.Normalize();

        //Попал ли по врагу
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, movement, distanceAttackSword);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Enemy>().Damage(swordDamage);
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
}
