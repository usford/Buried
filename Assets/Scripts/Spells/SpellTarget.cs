﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTarget : Spell
{
    [HideInInspector]
    public float damage; //Урон от способности
    [HideInInspector]
    public float spellSpeed; //Скорость полёта способности
    [HideInInspector]
    public Vector3 mousePosition;
    [HideInInspector]
    public Transform moution; //Точка спавна способности
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public SpellTargetInfo spellTargetInfo;

    private void Awake() 
    {
        spellTargetInfo = spellInfo.spell as SpellTargetInfo;

        var plusDamage = 20 * spellInfo.lvl;
        damage = spellTargetInfo.damage + plusDamage;

        var plusSpeed = 0.3f * spellInfo.lvl;
        spellSpeed = spellTargetInfo.spellSpeed + plusSpeed;
    }

    public override void ActivateSpell()
    {   
        isActive = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); 
        animator = GetComponent<Animator>();
        moution = GameObject.Find("Moution").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
               
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var difference = mousePosition.x - player.transform.position.x;

        if (spriteRenderer != null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipY = (difference > 0) ? false : true;
        }
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
        rb.AddForce(force * spellSpeed, ForceMode2D.Impulse);
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + (mousePosition * 3), Time.deltaTime * 10.0f);
    }

    //Уничтожить способность
    public virtual IEnumerator Destroy()
    {
        CheckDistance();
        rb.velocity = new Vector2 (0.0f, 0.0f);
        animator.SetTrigger("destroy");
        yield return new WaitForSeconds(0.5f);
        isActive = false;
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
