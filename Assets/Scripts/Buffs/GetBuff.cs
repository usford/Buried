using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBuff : MonoBehaviour
{
    public GameObject text;
    public GameObject buff;
    private Player player;
    private GameManager gameManager;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update() 
    {
        if (player != null && (Vector3.Distance(transform.position, player.transform.position) <= 1f))
        {
            text.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                CheckBuff();
            }
        }else
        {
           text.SetActive(false);
        }
    }

    //Есть ли уже такой баф
    private void CheckBuff()
    {
        bool isBuff = false;
        string nameBuff = "";
        bool check = true;
        player.buffs.ForEach((_buff) => 
        {
            if (buff.GetComponent<Buff>().buffInfo.nameBuff == _buff.GetComponent<Buff>().buffInfo.nameBuff)
            {
                isBuff = true;
                nameBuff = _buff.GetComponent<Buff>().buffInfo.nameBuff;
            }
        });

        if (!isBuff)
        {
            List<GameObject> list = player.buffs;
            buff.GetComponent<Buff>().buffInfo.isFound = true;
            #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(buff.GetComponent<Buff>().buffInfo); 
            #endif
            list.Add(buff);
            player.Buffs = list;
        }else
        {
            //Проверка на наличие бафа в иерархии (Иконки слева)
            Transform children = gameManager.ui.buffs.GetComponent<Transform>();

            foreach (Transform child in children)
            {
                if (child.GetComponent<Buff>().buffInfo.nameBuff == nameBuff)
                {
                    check = child.GetComponent<Buff>().RefreshBuff();
                }
            }
        }

        if (check) Destroy(gameObject);;
    }
}
