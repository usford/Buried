using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAura : Spell
{
    public float durationSpell = 5.0f; //Время действия способности

    public override void ActivateSpell()
    {

    }

    public virtual IEnumerator DeleteSpell()
    {
        yield break;
    }
}
