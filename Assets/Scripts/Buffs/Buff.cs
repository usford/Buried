using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    public string nameBuff; //Название бафа
    public float duration; //Время действия бафа
    public string description; //Описание бафа
    public GameObject imageTimer; //Таймер бафа
    private Image timer; //Картинка таймера

    [HideInInspector]
    public Player player;

    private float count;
    public UniqueNameBuff uniqueNameBuff; //Имя бафа, по которому можно его отследить

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        timer = imageTimer.GetComponent<Image>();
        StartCoroutine(DeleteBuff(duration));
    }

    //Срабатывание бафа с увеличением чего-то
    public virtual float ActuationBuffNumeric(float field)
    {
        return field;
    }

    //Баф направленный на таргет
    public virtual void ActuationBuffTarget(GameObject gameObject)
    {

    }

    //Удаление бафа
    private IEnumerator DeleteBuff(float duration)
    {
        count = 0;

        timer.fillAmount = 0;

        while (count < duration)
        {
            
            yield return new WaitForSeconds(0.05f);
            count += 0.05f;
            timer.fillAmount += 0.05f / duration;
        }

        //yield return new WaitForSeconds(duration);
        //CancelActionBuff();

        GameObject deleteBuff = new GameObject();
        player.buffs.ForEach((buff) =>
        {
            if (buff.GetComponent<Buff>().nameBuff == nameBuff)
            {
                deleteBuff = buff;
                return;
            }
        });
        player.buffs.Remove(deleteBuff);
        Destroy(gameObject);
    }

    //Отмена действия бафа
    public virtual void CancelActionBuff()
    {

    }

    //Обновление бафа
    public void RefreshBuff()
    {
        //StopCoroutine(coroutine);
        //StartCoroutine(coroutine);
        count = 0;
        timer.fillAmount = 0;
    }

    public enum TypeBuff
    {
        Numeric, //Числовые бафы
        Target, //Таргетные бафы
    }

    public enum UniqueNameBuff
    {
        Damage,
        PlayerSpeed,
        Freezing
    }
}
