using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealth : MonoBehaviour
{
    public float hpRecovery = 1.0f; //Восстановление хп
    public GameObject text;
    private Player player;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Update() 
    {
        //Rotation();
        Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        if ((Vector3.Distance(transform.position, player.transform.position) <= 0.8f))
        {
            text.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                player.CurrentHp += hpRecovery;
                Destroy(gameObject);
            }
        }else
        {
            text.SetActive(false);
        }
    }

    //Вращение склянки
    private void Rotation()
    {

        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime * 4f);
    }
}
