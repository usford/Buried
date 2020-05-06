using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeed : BuffNumeric
{
    public override float ActuationNumericBuff(float field)
    {
        var plusSpeed = field * (up + (0.1f * buffInfo.lvl));

        return plusSpeed;
    }
}
