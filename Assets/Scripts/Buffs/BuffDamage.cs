using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDamage : Buff
{
    public float upDamage; //Увеличение урона в процентах
    
    public override void ActuationBuff()
    {
        player.SwordDamage = (player.SwordDamage * upDamage) + player.SwordDamage;
    }

    public override void CancelActionBuff()
    {

    }
}
