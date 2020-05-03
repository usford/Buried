using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : Enemy
{
    private Vector3 direction;
    public GameObject fart;
    public bool isFart = true;

    private void Awake() 
    {
        direction = transform.right;   
    }
    private void OnEnable() 
    {
        StartCoroutine(RandomMove());
        if (isFart) StartCoroutine(SpawnFart());
    }
    private void FixedUpdate() 
    {
        Move();
        // Debug.Log($"Transform.right: {transform.right}");
        // Debug.Log($"-Transform.right: {-transform.right}");
        // Debug.Log($"Transform.up: {transform.up}");
        // Debug.Log($"-Transform.right: {-transform.up}");
        
    }

    //Спавн газов 
    private IEnumerator SpawnFart()
    {
        while(true)
        {  
            yield return new WaitForSeconds(0.3f);
            GameObject newFart = Instantiate(fart, transform.position, Quaternion.identity);
            newFart.transform.SetParent(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().boardScript.currentRoom.transform);
            newFart.GetComponent<Fart>().DeleteFart();
        }
    }

    
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * currentSpeed);
         
        //Проверка на наличие стены
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 0.75f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.transform == transform) continue;
            if (hit.collider.tag == "OuterWall" || hit.collider.tag == "Exit")
            {
                if (hit.collider.GetComponent<SpriteRenderer>().sortingLayerName == "Wall" 
                || hit.collider.GetComponent<SpriteRenderer>().sortingLayerName == "Exit"
                || hit.collider.tag == "Enemy")
                {
                    //Debug.Log("СТЕНА СТЕНА СТЕНА");

                    changeDir();
                }
            }
        }
    }

    //Шанс на произвольный смен курса
    private IEnumerator RandomMove()
    {
        float chance = 0.3f;
        while(true)
        {
            float random = Random.Range(0.0f, 1.0f);

            if (random >= chance)
            {
                changeDir();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void changeDir()
    {
        int random;
        switch(direction.ToString())
                {
                    //Шёл вправо
                    case "(1.0, 0.0, 0.0)":
                    {
                        random = Random.Range(0, 2);

                        direction = (random == 0) ? transform.up : -transform.up;
                        break;
                    }
                    //Шёл влево
                    case "(-1.0, 0.0, 0.0)":
                    {
                        random = Random.Range(0, 2);

                        direction = (random == 0) ? transform.up : -transform.up;
                        break;
                    }
                    //Шёл вверх
                    case "(0.0, 1.0, 0.0)":
                    {
                        random = Random.Range(0, 2);

                        direction = (random == 0) ? transform.right : -transform.right;
                        GetComponent<SpriteRenderer>().flipX = (direction == transform.right) ? false : true;
                        break;
                    }
                    //Шёл вниз
                    case "(0.0, -1.0, 0.0)":
                    {
                        random = Random.Range(0, 2);

                        direction = (random == 0) ? transform.right : -transform.right;

                        GetComponent<SpriteRenderer>().flipX = (direction == transform.right) ? false : true;
                        break;
                    }
                }
    }
}
