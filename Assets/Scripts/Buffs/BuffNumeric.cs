using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffNumeric : Buff
{
    public float up; //Увеличение

    public override float ActuationNumericBuff(float field)
    {
        return field;
    }
}
