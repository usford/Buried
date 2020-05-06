using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeed : BuffNumeric
{
    public override float ActuationNumericBuff(float field)
    {
        var plusSpeed = field * up;

        plusSpeed += plusSpeed * ((float)buffInfo.lvl / 6);

        return plusSpeed;
    }
}
