using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public int roomX = 1;
    public int roomY = 1;
    private BoardManager boardScript;

    private void Awake()
    {
        boardScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().boardScript;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boardScript.MapRendering(roomX, roomY);
        }
    }
}
