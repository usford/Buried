using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fart : MonoBehaviour
{
    public float damage = 10.0f; //Урон от газа
    public float powerForce = 45.0f; //Сила толчка
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().ReceiveDamage(damage);
        }
    }

    //Удаление газов
    public void DeleteFart()
    {
        StartCoroutine(DeleteFartTime());
    }

    private IEnumerator DeleteFartTime()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
