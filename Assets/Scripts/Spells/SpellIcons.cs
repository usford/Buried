using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellIcons : MonoBehaviour
{
    public string nameSpell; //Наименование способности
    public KeyCode spellBtn; //Кнопка по нажатию на которую создаётся способность
    public float coolDown = 10.0f; //Время перезарядки способности
    private bool coolDownCheck = false;
    public Image imageCoolDown;

    private void Update() 
    {
        if (Input.GetKeyDown(spellBtn) && nameSpell != "" && !coolDownCheck)
        {
            CreateSkill(nameSpell);
        }
    }

    //Создание скилла
    private void CreateSkill(string nameSpell)
    {
        Instantiate(Resources.Load<GameObject>($"Spells/{nameSpell}"));
        StartCoroutine(timeCoolDown());
    }

    //Время перезарядки способности
    private IEnumerator timeCoolDown()
    {
        coolDownCheck = true;

        imageCoolDown.gameObject.SetActive(true);

        float count = 0;
        imageCoolDown.fillAmount = coolDown / coolDown;

        while (count < coolDown)
        {
            
            yield return new WaitForSeconds(0.05f);
            count += 0.05f;
            imageCoolDown.fillAmount -= 0.05f / coolDown;
        }

        imageCoolDown.gameObject.SetActive(false);

        coolDownCheck = false;
    }
}
