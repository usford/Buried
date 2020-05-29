using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneEyed : Enemy
{
    public GameObject placeOfAttack; //Место, откуда произведётся выстрел
    public GameObject shot; //Префаб выстрела
    private Vector2 direction;
    private SpriteRenderer spriteRenderer;
    
    private void OnEnable() 
    {
        float delay = Random.Range(1.0f, 3.0f);
        direction = new Vector2(1.0f, 0.5f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Attack(delay));
    }

    private void FixedUpdate() 
    {
        Move();  
    }

    private void Move()
    {
        GetComponent<Rigidbody2D>().velocity = direction * currentSpeed;
    }

    private IEnumerator Attack(float delay)
    {
        yield return new WaitForSeconds(delay);

        while(true && player != null)
        {
            GameObject _shot = Instantiate(shot, placeOfAttack.transform.position, Quaternion.identity);
            _shot.GetComponent<AttackOneEyed>().damage = enemyInfo.damage;

            float x = player.transform.position.x - _shot.transform.position.x;
            float y = player.transform.position.y - _shot.transform.position.y;
            //Debug.Log("X: " + x);
            //Debug.Log("Y: " + y);

            Vector2 force = new Vector2(x, y);
            _shot.GetComponent<Rigidbody2D>().AddForce(force * 1.5f, ForceMode2D.Impulse);

            float randomDelay = Random.Range(2.0f, 3.0f);
            yield return new WaitForSeconds(randomDelay);
        }
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "OuterWall")
        {
            //Debug.Log(direction.ToString());
            switch(direction.ToString())
            {
                case "(1.0, 0.5)":
                {
                    direction = new Vector2(-1.0f, 0.9f);
                    spriteRenderer.flipX = true;
                    break;
                } 
                case "(-1.0, 0.9)":
                {
                    direction = new Vector2(-1.0f, -0.5f);
                    break;
                } 
                case "(-1.0, -0.5)":
                {
                    direction = new Vector2(1.0f, -0.9f);
                    spriteRenderer.flipX = false;
                    break;
                }
                case "(1.0, -0.9)":
                {
                    direction = new Vector2(1.0f, 0.5f);
                    spriteRenderer.flipX = false;
                    break;
                } 
                default: break;
            }
        }
    }
}
