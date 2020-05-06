using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffGold : BuffNumeric
{
    public override float ActuationNumericBuff(float field)
    {
        var plusGold = field * (up + (0.5f * buffInfo.lvl));      

        return plusGold;
    }
}
