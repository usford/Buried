using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeholder : Enemy
{
    private Vector3 direction;
    public bool isUp; //true - верх/вниз false - влево/вправо
    private bool directionUp;

    public Vector3 attackDirection;
    private bool isAttack = false; //Враг атакует
    private bool isAttackDelay = false; //После выстрела
    private float distance;
    public GameObject attackRay;
    private float onePoint; 
    private float twoPoint;
    private Animator animator;

    private void Awake() 
    { 
        onePoint = (isUp) ? 0.3f : 0.5f;
        twoPoint = (isUp) ? 1.0f : 0.5f;
        directionUp = true;
        direction = (isUp) ? transform.up : transform.up;

        distance = (directionUp) ? onePoint : twoPoint;

        animator = GetComponent<Animator>();
    }
    private void FixedUpdate() 
    {
        if (!isAttack) Move();
        if (!isAttack && !isAttackDelay) FindPlayer();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * currentSpeed);

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, distance);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.transform == transform) continue;
            if (hit.collider.tag == "OuterWall" || hit.collider.tag == "Enemy")
            {
                direction = (directionUp) ? -direction : direction;
                directionUp = !directionUp;
                distance = (directionUp) ? onePoint : twoPoint;

            }
        }
    }

    private void FindPlayer()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, attackDirection);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == "Player")
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttack = true;
        isAttackDelay = true;
        animator.SetTrigger("attack");

        yield return new WaitForSeconds(0.5f);

        attackRay.SetActive(true);

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, attackDirection);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == "Player")
            {
                Hit();
            }
        }

        yield return new WaitForSeconds(0.5f);
        attackRay.SetActive(false);
        isAttack = false;

        yield return new WaitForSeconds(1f);
        isAttackDelay = false;
    }
}
