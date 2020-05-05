using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int amount; //Количество денег
    public int Amount
    {
        get
        {
            return amount;
        }
        set
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            float plusGold = 0.0f;

            bool check = player.CheckBuff(Buff.UniqueNameBuff.Gold);

            if (check)
            {
                plusGold = player.ActivateNumericBuff(Buff.UniqueNameBuff.Gold, value);
                //Debug.Log(plusGold);
            }

            amount = value + (int)plusGold; 
            SwitchSprite(amount);
        }
    }
    public Sprite[] goldSprites; //Все спрайты с золотом
    private Player player;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        SwitchSprite(amount);
    }

    //Изменение спрайта в зависимости от кол-ва денег
    private void SwitchSprite(int amount)
    {
        switch(amount)
        {
            case var n when (n <= 5 && n > 0):
            {
                GetComponent<SpriteRenderer>().sprite = goldSprites[0];
                break;
            }
            
            case var n when (n <= 10 && n > 5):
            {
                GetComponent<SpriteRenderer>().sprite = goldSprites[1];
                break;
            }

            case var n when (n <= 15 && n > 10):
            {
                GetComponent<SpriteRenderer>().sprite = goldSprites[2];
                break;
            }
        }

        int random = Random.Range(0, 2);  
        GetComponent<SpriteRenderer>().flipX = (random == 0) ? true : false;   
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //Герой подбирает золота
        if (other.tag == "Player")
        {
            player.AmountGold = player.amountGold + amount;
            Destroy(gameObject);
        }
    }
}
