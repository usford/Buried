﻿using System.Collections;
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

        float count = 0.0f;
        bool state = true;
        float leftTime = durationSpell - (durationSpell * 0.3f);

        Color color = GetComponent<SpriteRenderer>().color;

        while (count < durationSpell)
        {
            count += 0.1f;
            if (count > leftTime)
            {       
                state = !state;
                int a = (state) ? 1 : 0;
                color.a = a;

                GetComponent<SpriteRenderer>().color = color;
            }
            yield return new WaitForSeconds(0.1f);
        }

        
        isActive = false;
        Destroy(gameObject);  
    }
}