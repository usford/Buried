﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDamage : Buff
{
    public float upDamage; //Увеличение урона в процентах
    
    public override float ActuationBuffNumeric(float field)
    {
        //player.SwordDamage = (player.SwordDamage * upDamage) + player.SwordDamage;

        var plusDamage = field * upDamage;

        return plusDamage;
    }
}
