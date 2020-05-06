using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDamage : BuffNumeric
{ 
    public override float ActuationNumericBuff(float field)
    {
        var plusDamage = field * (up + (0.1f * buffInfo.lvl));

        return plusDamage;
    }
}
