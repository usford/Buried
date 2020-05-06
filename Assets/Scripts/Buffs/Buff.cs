using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    [HideInInspector]
    private Image timer; //Картинка таймера
    [HideInInspector]
    public Player player;

    private float count;
    public UniqueNameBuff uniqueNameBuff; //Имя бафа, по которому можно его отследить
    public BuffInfo buffInfo;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Image[] children = GetComponentsInChildren<Image>();

        timer = children[1];
        if (!buffInfo.isEndless) StartCoroutine(DeleteBuff(buffInfo.duration));
    }

    //Срабатывание бафа с увеличением чего-то
    public virtual float ActuationNumericBuff(float field)
    {
        return field;
    }

    //Срабатывание таргетного бафа
    public virtual void ActuationTargetBuff(GameObject enemy)
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
            if (buff.GetComponent<Buff>().buffInfo.nameBuff == buffInfo.nameBuff)
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

    public enum UniqueNameBuff
    {
        Damage,
        PlayerSpeed,
        Freezing,
        Gold,
    }
}
