using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeed : Buff
{
    public float upSpeed; //Увеличение скорости в процентах
    
    public override float ActuationBuffNumeric(float field)
    {
        //player.SwordDamage = (player.SwordDamage * upDamage) + player.SwordDamage;

        var plusSpeed = field * upSpeed;

        return plusSpeed;
    }
}
