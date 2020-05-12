using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Image icon; //Иконка улучшения
    public Text nameUpgrade; //Название улучшения
    private int lvl; //Уровень способности
    private int priceLvl; //Цена за улучшение
    private int maxLvl; //Максимальное количество улучшений
    public ScriptableObject itemUpgrade; //Улучшение
    public Text textUpgrade;
    public TypeUpgrade type;
    public GameObject levelsUpgrade; //Уровни улучшения (визуал)
    public GameObject lvlUpgrade; //Улучшение
    public Text descriptionUpgrade;
    private Player player;
    public GameObject locked; //Закрыта ли способности

    

    private void Awake() 
    {
        //Refresh();
    }

    //Улучшить способности
    public void Upgraded()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (player.AmountGold - (priceLvl * (lvl + 1)) >= 0)
        {
            switch(type)
            {
                case TypeUpgrade.Spell:
                {
                    var item = itemUpgrade as SpellInfo;
                    if (item.lvl + 1 <= maxLvl)
                    {
                        item.lvl += 1;
                        lvl = item.lvl;
                        CheckLvlUpgrade();
                        #if UNITY_EDITOR
                        UnityEditor.EditorUtility.SetDirty(item); 
                        #endif
                    }
                    break;
                }

                case TypeUpgrade.Buff:
                {
                    var item = itemUpgrade as BuffInfo;
                    if (item.lvl + 1 <= maxLvl)
                    {
                        item.lvl += 1;
                        lvl = item.lvl;
                        CheckLvlUpgrade();
                        #if UNITY_EDITOR
                        UnityEditor.EditorUtility.SetDirty(item); 
                        #endif
                    }
                    break;
                }
            }   

            player.AmountGold -= priceLvl * (lvl);
            Refresh();
        }
    }

    public void Refresh()
    {
        switch(type)
        {
            case TypeUpgrade.Spell:
            {
                var item = itemUpgrade as SpellInfo;
                icon.sprite = item.icon;
                nameUpgrade.text = item.nameSpell;
                priceLvl = item.priceLvl;
                descriptionUpgrade.text = item.descriptionUpgrade;
                lvl = item.lvl;
                textUpgrade.text = $"Улучшить {priceLvl * (lvl + 1)}";
                maxLvl = item.maxLvl;
                break;
            }

            case TypeUpgrade.Buff:
            {
                var item = itemUpgrade as BuffInfo;
                icon.sprite = item.icon;
                nameUpgrade.text = item.nameBuff;
                priceLvl = item.priceLvl;
                descriptionUpgrade.text = item.descriptionUpgrade;
                lvl = item.lvl;
                textUpgrade.text = $"Улучшить {priceLvl * (lvl + 1)}";
                maxLvl = item.maxLvl;
                break;
            }
        }
        
        CheckLvlUpgrade();
    }

    //Проверка уровня улучшений
    public void CheckLvlUpgrade()
    {
        Transform children = levelsUpgrade.GetComponent<Transform>();
        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxLvl; i++)
        {
            Color color = (i < lvl) ? Color.yellow : Color.white;

            GameObject _lvlUpgrade = Instantiate(lvlUpgrade);

            _lvlUpgrade.GetComponent<Image>().color = color;

            _lvlUpgrade.transform.SetParent(levelsUpgrade.transform);

            Vector3 scale = _lvlUpgrade.transform.localScale;
            scale.x = 1; 
            scale.y = 1; 
            scale.z = 1;
            _lvlUpgrade.transform.localScale = scale;
        }

        if (lvl == maxLvl)
        {
            descriptionUpgrade.text = "Максимум улучшений";
            textUpgrade.text = $"Максимальная прокачка";
        }
    }

    public enum TypeUpgrade
    {
        Spell,
        Buff,
        Hero
    }
}
