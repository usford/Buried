using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffFreezing : Buff
{
    public float freezeTime = 0.5f; //Сколько персонаж будет заморожен
    public float freezeSpeed = 0.3f; //На сколько персонаж будет медленее двигаться
    private bool coroutineRun = false; //Идёт ли коротуни
    private float timerCount; //Отсчёт для поддержания заморозки

    public override void ActuationBuffTarget(GameObject enemy)
    {
        //StartCoroutine(AnimatiomFreez(enemy));
        //Debug.Log(coroutineRun);
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        // if (!coroutineRun)
        // {
        //     StartCoroutine(AnimationFreeze(enemy));
        // }else
        // {
        //     timerCount = 0;
        // }
        StartCoroutine(AnimationFreeze(enemy));
    }

    //Анимация заморозки врага
    public IEnumerator AnimationFreeze(GameObject enemy)
    {
        float minusSpeed = enemy.GetComponent<Enemy>().maxSpeed * 0.3f;
        enemy.GetComponent<Enemy>().currentSpeed =  enemy.GetComponent<Enemy>().maxSpeed - minusSpeed;
        // Debug.Log($"maxSpeed:{enemy.GetComponent<Enemy>().maxSpeed }");
        // Debug.Log($"currentSpeed:{enemy.GetComponent<Enemy>().currentSpeed }");
        // Debug.Log($"minusSpeed:{minusSpeed}");
        coroutineRun = true;  

        timerCount = 0;
        while (timerCount < freezeTime)
        {
            timerCount += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        if (enemy == null) yield break;
        enemy.GetComponent<SpriteRenderer>().color = Color.white;
        coroutineRun = false;

        enemy.GetComponent<Enemy>().currentSpeed = enemy.GetComponent<Enemy>().maxSpeed;
    }
}
