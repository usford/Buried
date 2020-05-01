using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private Animator animator;
    private bool spiked = false; //Находится ли игрок на шипах
    private float damage = 1.0f; //Урон от шипов
    private bool isPlay = false; //Проигрывается ли анимация

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
            StartCoroutine(Hit(other));
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            spiked = false;
        }
    }

    //Нанесение урона от шипов
    private IEnumerator Hit(Collider2D player)
    {
        isPlay = true;
        spiked = true;
        animator.SetTrigger("activate");
        yield return new WaitForSeconds(0.7f);
        if (spiked) player.GetComponent<Player>().ReceiveDamage(damage); 
        isPlay = false;

        yield return new WaitForSeconds(0.4f);
        if (spiked) StartCoroutine(Hit(player));
    }
}
