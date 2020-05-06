using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffFreezing : BuffTarget
{
    [HideInInspector]
    public float freezeTime; //Сколько персонаж будет заморожен
    [HideInInspector]
    public float freezeSpeed; //На сколько персонаж будет медленее двигаться
    [HideInInspector]
    private BuffFreezingDetails thisBuff;


    private void Awake() 
    {
        thisBuff = buffInfo.buff as BuffFreezingDetails;
        freezeTime = thisBuff.freezeTime;
        freezeSpeed = thisBuff.freezeSpeed;

        freezeTime += freezeTime * ((float)buffInfo.lvl / 6);
        freezeSpeed += freezeSpeed * ((float)buffInfo.lvl / 3);
    }
    
    public override void ActuationTargetBuff(GameObject enemy)
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        StartCoroutine(AnimationDebuff(enemy));
    }

    //Анимация заморозки врага
    public override IEnumerator AnimationDebuff(GameObject enemy)
    {
        float minusSpeed = enemy.GetComponent<Enemy>().maxSpeed * freezeSpeed;
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
