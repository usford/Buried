using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDamage : BuffNumeric
{ 
    public override float ActuationNumericBuff(float field)
    {
        var plusDamage = field * up;

        plusDamage += plusDamage * ((float)buffInfo.lvl);

        return plusDamage;
    }
}
