using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfo : MonoBehaviour
{
    public string title; //Название сверху панели
    public Text titleText;
    public InfoHelper infoHelper;
    public GameObject content;
    public GameObject ActionInfo;
    public GameObject panelDescription; //Подробное описание

    private void OnEnable() 
    {
        titleText.text = title;

        Transform children = content.GetComponent<Transform>();
        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }

        switch (title)
        {
            case "Способности":
            {
                CreateButtonSpell();    
                break;
            }

            case "Бафы":
            {
                CreateButtonBuff();    
                break;
            }

            case "Враги":
            {
                CreateButtonEnemy();    
                break;
            }
        }
    }

    //Кнопка для показа информации о способностях
    private void CreateButtonSpell()
    {
        for (int i = 0; i < infoHelper.spells.Count; i++)
        {
            GameObject spell = Instantiate(ActionInfo);
            spell.GetComponent<ActionInfo>().icon.sprite = infoHelper.spells[i].icon;
            spell.GetComponent<ActionInfo>().nameInfo.text = infoHelper.spells[i].nameSpell;

            int temp = i;
            spell.AddComponent<Button>();
            spell.GetComponent<Button>().onClick.AddListener(
                () => ShowPanelInfo(infoHelper.spells[temp].icon, 
                infoHelper.spells[temp].nameSpell, 
                infoHelper.spells[temp].description)
            );        

            spell.transform.SetParent(content.transform);

            Vector3 scale = spell.transform.localScale;
            scale.x = 1; 
            scale.y = 1; 
            scale.z = 1;
            spell.transform.localScale = scale;
        }

        ShowPanelInfo(infoHelper.spells[0].icon, infoHelper.spells[0].nameSpell, infoHelper.spells[0].description);
    }

    //Кнопка для показа информации о бафах
    private void CreateButtonBuff()
    {
        for (int i = 0; i < infoHelper.buffs.Count; i++)
        {
            GameObject buff = Instantiate(ActionInfo);
            buff.GetComponent<ActionInfo>().icon.sprite = infoHelper.buffs[i].icon;
            buff.GetComponent<ActionInfo>().nameInfo.text = infoHelper.buffs[i].nameBuff;

            int temp = i;
            buff.AddComponent<Button>();
            buff.GetComponent<Button>().onClick.AddListener(
                () => ShowPanelInfo(infoHelper.buffs[temp].icon, 
                infoHelper.buffs[temp].nameBuff, 
                infoHelper.buffs[temp].description)
            );        

            buff.transform.SetParent(content.transform);

            Vector3 scale = buff.transform.localScale;
            scale.x = 1; 
            scale.y = 1; 
            scale.z = 1;
            buff.transform.localScale = scale;
        }

        ShowPanelInfo(infoHelper.buffs[0].icon, infoHelper.buffs[0].nameBuff, infoHelper.buffs[0].description);
    }

    //Кнопка для показа информации о врагах
    private void CreateButtonEnemy()
    {
        for (int i = 0; i < infoHelper.enemies.Count; i++)
        {
            GameObject enemy = Instantiate(ActionInfo);
            enemy.GetComponent<ActionInfo>().icon.sprite = infoHelper.enemies[i].icon;
            enemy.GetComponent<ActionInfo>().nameInfo.text = infoHelper.enemies[i].nameEnemy;

            int temp = i;
            enemy.AddComponent<Button>();
            enemy.GetComponent<Button>().onClick.AddListener(
                () => ShowPanelInfo(infoHelper.enemies[temp].icon, 
                infoHelper.enemies[temp].nameEnemy, 
                infoHelper.enemies[temp].description)
            );        

            enemy.transform.SetParent(content.transform);

            Vector3 scale = enemy.transform.localScale;
            scale.x = 1; 
            scale.y = 1; 
            scale.z = 1;
            enemy.transform.localScale = scale;
        }

        ShowPanelInfo(infoHelper.enemies[0].icon, infoHelper.enemies[0].nameEnemy, infoHelper.enemies[0].description);
    }

    //Показать окно с информацией
    private void ShowPanelInfo(Sprite icon, string nameInfo, string description)
    {
        panelDescription.GetComponent<PanelDescription>().icon.sprite = icon;
        panelDescription.GetComponent<PanelDescription>().nameInfo.text = nameInfo;
        panelDescription.GetComponent<PanelDescription>().description.text = description;
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }

    
}
