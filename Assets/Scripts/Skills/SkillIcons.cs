using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIcons : MonoBehaviour
{
    public string nameSkill = ""; //Наименование способности

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreateSkill("Comet");
        }
    }

    //Создание скилла
    private void CreateSkill(string nameSkill)
    {
        Instantiate(Resources.Load<GameObject>($"Skills/{nameSkill}"));
    }
}
