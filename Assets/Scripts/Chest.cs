using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private GameObject player;

    public GameObject text;
    public Sprite openChest;
    public bool stateChest = false;
    private GameManager gameManager;
    public List<GameObject> awardsSpells;
    public List<GameObject> awardsBuffs; 
    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update() 
    {
        if ((Vector3.Distance(transform.position, player.transform.position) <= 1.0f))
        {
            text.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && !stateChest)
            {
                GetComponent<Animator>().enabled = false;
                text.GetComponent<Text>().text = "";
                GetComponent<SpriteRenderer>().sprite = openChest;
                stateChest = true;
                gameManager.playerStatistics.openChests += 1;
                OpenChest();
            }
        }else
        {
            text.SetActive(false);
        }
    }

    private void OpenChest()
    {
        int randomCount = Random.Range(1, 3);
        for (int i = 0; i < randomCount; i++)
        {
            float randomType = Random.Range(0.0f, 1.1f);
            if (randomType <= 0.3f)
            {
                int randomAward = Random.Range(0, awardsSpells.Count);
                Instantiate(awardsSpells[randomAward], transform.position, Quaternion.identity);
                awardsSpells.Remove(awardsSpells[randomAward]);
            }else
            {
                int randomAward = Random.Range(0, awardsBuffs.Count);
                Instantiate(awardsBuffs[randomAward], transform.position, Quaternion.identity);
                awardsBuffs.Remove(awardsBuffs[randomAward]);
            }
        }
    }
}
