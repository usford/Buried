using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public GameObject spellsPanel;
    public GameObject contentSpells;
    public GameObject buffsPanel;
    public GameObject contentBuffs;
    private GameManager gameManager;
    public GameObject upgrade;

    public void Exit()
    {
        gameObject.SetActive(false);
    }

    public void Spells()
    {
        CheckSpells();
        gameObject.SetActive(false);
        spellsPanel.SetActive(true);
    }

    public void Buffs()
    {
        CheckBuffs();
        gameObject.SetActive(false);
        buffsPanel.SetActive(true);
    }

    public void CheckSpells()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Transform children = contentSpells.GetComponent<Transform>();
        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }

        Vector2 sizeDelta = new Vector2();
        sizeDelta.y = 0.0f;

        Vector2 localPosition = new Vector2();
        localPosition.y = 0.0f;
        contentBuffs.GetComponent<RectTransform>().localPosition = localPosition;

        gameManager.allSpells.Sort((a, b) => b.isFound.CompareTo(a.isFound));

        for (int i = 0; i < gameManager.allSpells.Count; i++)
        {
            GameObject spell = Instantiate(upgrade);

            spell.GetComponent<Upgrade>().type = Upgrade.TypeUpgrade.Spell;

            spell.GetComponent<Upgrade>().itemUpgrade = gameManager.allSpells[i];
            if (gameManager.allSpells[i].isFound) spell.GetComponent<Upgrade>().locked.SetActive(false);

            spell.transform.SetParent(contentSpells.transform);

            Vector3 scale = spell.transform.localScale;
            scale.x = 1; 
            scale.y = 1; 
            scale.z = 1;
            spell.transform.localScale = scale;

            spell.GetComponent<Upgrade>().Refresh();

            
            sizeDelta.y += 100.0f; 
        }
        contentBuffs.GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    public void CheckBuffs()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Transform children = contentBuffs.GetComponent<Transform>();
        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }

        Vector2 sizeDelta = new Vector2();
        sizeDelta.y = 0.0f;

        Vector2 localPosition = new Vector2();
        localPosition.y = 0.0f;
        contentBuffs.GetComponent<RectTransform>().localPosition = localPosition;

        gameManager.allBuffs.Sort((a, b) => b.isFound.CompareTo(a.isFound));

        for (int i = 0; i < gameManager.allBuffs.Count; i++)
        {
            GameObject buff = Instantiate(upgrade);

            buff.GetComponent<Upgrade>().type = Upgrade.TypeUpgrade.Buff;

            buff.GetComponent<Upgrade>().itemUpgrade = gameManager.allBuffs[i];

            if (gameManager.allBuffs[i].isFound) buff.GetComponent<Upgrade>().locked.SetActive(false);

            buff.transform.SetParent(contentBuffs.transform);

            Vector3 scale = buff.transform.localScale;
            scale.x = 1; 
            scale.y = 1; 
            scale.z = 1;
            buff.transform.localScale = scale;

            buff.GetComponent<Upgrade>().Refresh();

            sizeDelta.y += 115.0f; 
        }

        contentBuffs.GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }
}
