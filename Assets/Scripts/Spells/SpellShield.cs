using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellShield : Spell
{
    public float durationSpell = 5.0f;
    public override void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(DeleteSpell());
        transform.position = player.transform.position;
    }

    private void Update() 
    {
        transform.position = player.transform.position;
    }

    private IEnumerator DeleteSpell()
    {  
        isActive = true;
        yield return new WaitForSeconds(durationSpell);
        isActive = false;
        Destroy(gameObject);  
    }
}
