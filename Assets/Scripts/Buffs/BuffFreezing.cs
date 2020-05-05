using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffFreezing : BuffTarget
{
    public float freezeTime = 0.5f; //Сколько персонаж будет заморожен
    public float freezeSpeed = 0.3f; //На сколько персонаж будет медленее двигаться
    
    public override dynamic ActuationBuff(dynamic enemy)
    {

        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        StartCoroutine(AnimationDebuff(enemy));
        return 0;
    }

    //Анимация заморозки врага
    public override IEnumerator AnimationDebuff(GameObject enemy)
    {
        float minusSpeed = enemy.GetComponent<Enemy>().maxSpeed * 0.3f;
        enemy.GetComponent<Enemy>().currentSpeed =  enemy.GetComponent<Enemy>().maxSpeed - minusSpeed;
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
