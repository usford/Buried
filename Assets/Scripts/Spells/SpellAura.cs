using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAura : Spell
{
    [HideInInspector]
    public float durationSpell; //Время действия способности
    [HideInInspector]
    public SpellAuraInfo spellAuraInfo;

    private void Awake() {
        spellAuraInfo = spellInfo.spell as SpellAuraInfo;

        var plusDuration = spellAuraInfo.durationSpell * (spellInfo.lvl * 0.1f);
        durationSpell = spellAuraInfo.durationSpell + plusDuration;
    }

    public override void ActivateSpell()
    {

    }

    public virtual IEnumerator DeleteSpell()
    {
        yield break;
    }
}
