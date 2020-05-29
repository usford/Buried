using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOneEyed : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    private Rigidbody2D rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().ReceiveDamage(damage);
            Destroy(gameObject);
        }else if (other.tag == "OuterWall" || other.tag == "Exit")
        {
            Destroy(gameObject);
        }
    }
}
