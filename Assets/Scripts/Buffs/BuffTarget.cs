using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTarget : Buff
{
    [HideInInspector]
    public bool coroutineRun = false; //Идёт ли коротуна
    [HideInInspector]
    public float timerCount; //Отсчёт для поддержания дебаффа

    public override dynamic ActuationBuff(dynamic enemy)
    {
        return 0;
    }

    //Анимация дебаффа
    public virtual IEnumerator AnimationDebuff(GameObject enemy)
    {
        yield break;
    }
}
