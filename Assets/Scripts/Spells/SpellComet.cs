using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellComet : SpellTarget
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "OuterWall" || other.tag == "Exit" || other.tag == "Enemy")
        {
            StartCoroutine(Destroy());
        }
    }
}
