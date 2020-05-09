using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    public GameObject text;
    public GameObject spell;
    private Player player;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update() 
    {
        if ((Vector3.Distance(transform.position, player.transform.position) <= 1f))
        {
            text.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                List<GameObject> list = player.Spells;
                spell.GetComponent<Spell>().spellInfo.isFound = true;
                list.Add(spell);    
                player.Spells = list;
                Destroy(gameObject);
            }
        }else
        {
            text.SetActive(false);
        }
    }
}
