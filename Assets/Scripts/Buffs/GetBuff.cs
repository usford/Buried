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
        if ((Vector3.Distance(transform.position, player.transform.position) <= 1f))
        {
            text.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                CheckBuff();
                
                Destroy(gameObject);
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
        player.buffs.ForEach((_buff) => 
        {
            if (buff.GetComponent<Buff>().nameBuff == _buff.GetComponent<Buff>().nameBuff)
            {
                isBuff = true;
                nameBuff = _buff.GetComponent<Buff>().nameBuff;
            }
        });

        if (!isBuff)
        {
            List<GameObject> list = player.buffs;
            list.Add(buff);
            player.Buffs = list;
        }else
        {
            //Проверка на наличие бафа в иерархии (Иконки слева)
            Transform children = gameManager.ui.buffs.GetComponent<Transform>();

            foreach (Transform child in children)
            {
                if (child.GetComponent<Buff>().nameBuff == nameBuff)
                {
                    child.GetComponent<Buff>().RefreshBuff();
                }
            }
        }
    }
}
