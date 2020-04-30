using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public string nameSpell = "";
    public float damage = 20.0f; //Урон от способности
    public float skillSpeed = 15.0f; //Скорость полёта способности
    public float coolDown = 10.0f; //Время перезарядки способности
    public Player player;
    public Vector3 mousePosition;
    public Transform moution; //Точка спавна способности
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Sprite icon;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        moution = GameObject.Find("Moution").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var difference = mousePosition.x - player.transform.position.x;
        
        spriteRenderer.flipY = (difference > 0) ? false : true;

        Preparation();  
        Moution();
    }

    //Подготовка способности к движению
    private void Preparation()
    {
        // mousePosition = Input.mousePosition;
        // mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var angle = Vector2.Angle(Vector2.right, moution.position - player.transform.position);

        transform.rotation = Quaternion.Euler(0f, 0f, player.transform.position.y < moution.position.y ? angle : -angle);
        transform.position = moution.transform.position;
    }

    //Движение способности
    private void Moution()
    {
        float x = moution.position.x - player.transform.position.x;
        float y = moution.position.y - player.transform.position.y;

        Vector2 force = new Vector2(x, y);
        rb.AddForce(force * skillSpeed, ForceMode2D.Impulse);
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + (mousePosition * 3), Time.deltaTime * 10.0f);
    }

    //Уничтожить способность
    public virtual IEnumerator Destroy()
    {
        CheckDistance();
        rb.velocity = new Vector2 (0.0f, 0.0f);
        animator.SetTrigger("destroy");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    //Проверка, если кто-нибудь в радиусе поражения
    private void CheckDistance()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.8f);

		foreach(Collider2D hit in colliders)
		{
			Hit(hit);
		}
    }

    //Нанесение урона
    private void Hit(Collider2D hit)
    {
        switch(hit.tag)
        {
            case "Player":
            {
                hit.GetComponent<Player>().ReceiveDamage(damage / damage);
                break;
            }

            case "Enemy":
            {
                hit.GetComponent<Enemy>().ReceiveDamage(damage);
                break;
            }

            default: break;
        }

    }

    //Проверка
    public virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "OuterWall")
        {
            StartCoroutine(Destroy());
        }
    }
}
