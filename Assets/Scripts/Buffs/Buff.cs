using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public string nameBuff; //Название бафа
    public float duration; //Время действия баффа

    [HideInInspector]
    public Player player;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ActuationBuff();
        StartCoroutine(DeleteBuff(duration));
    }

    //Срабатывание бафа
    public virtual void ActuationBuff()
    {

    }

    //Удаление бафа
    private IEnumerator DeleteBuff(float duration)
    {
        yield return new WaitForSeconds(duration);
        CancelActionBuff();
        Destroy(gameObject);
    }

    //Отмена действия бафа
    public virtual void CancelActionBuff()
    {

    }
}
