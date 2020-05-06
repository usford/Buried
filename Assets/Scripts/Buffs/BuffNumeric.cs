using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffNumeric : Buff
{
    [HideInInspector]
    public float up; //Увеличение
    [HideInInspector]
    public BuffNumericInfo buffNumericInfo;

    private void Awake() 
    {
        buffNumericInfo = buffInfo.buff as BuffNumericInfo;
        up = buffNumericInfo.up;
    }

    public override float ActuationNumericBuff(float field)
    {
        return field;
    }
}
