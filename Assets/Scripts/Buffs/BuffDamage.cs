using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDamage : BuffNumeric
{ 
    public override dynamic ActuationBuff(dynamic field)
    {
        var plusDamage = field * up;

        return plusDamage;
    }
}
