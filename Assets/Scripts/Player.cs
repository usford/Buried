using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed = 2f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Horizontal")) Run("Horizontal");
        if (Input.GetButton("Vertical")) Run("Vertical");
    }

    void Run(string type)
    {
        Vector3 direction = (type == "Horizontal")
            ? transform.right * Input.GetAxis("Horizontal")
            : transform.up * Input.GetAxis("Vertical");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * maxSpeed);

        if (type == "Horizontal")
        {
            spriteRenderer.flipX = (direction.x < 0.0f) ? true : false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
    }
}
