using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeed : BuffNumeric
{
    public override dynamic ActuationBuff(dynamic field)
    {
        var plusSpeed = field * up;

        return plusSpeed;
    }
}
