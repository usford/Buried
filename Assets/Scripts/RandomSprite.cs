using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    private List<Sprite> sprites;
    public bool isRandom = true;
    public SpriteType spriteType;
    public string level = "RandomLevel1";
    

    private void Start() 
    {
        if (isRandom)
        {   
            switch(spriteType)
            {
                case SpriteType.floor:
                {
                    sprites = GameObject.Find(level).GetComponent<RandomGenerate>().floorSprites;
                    break;
                }

                case SpriteType.outerWall:
                {
                    sprites = GameObject.Find(level).GetComponent<RandomGenerate>().outerWallSprites;
                    break;
                }
                default: break;
            }
            Sprite randomSprite = sprites[Random.Range(0, sprites.Count)];
            GetComponent<SpriteRenderer>().sprite = randomSprite;
        }
    }

    //Тип спрайта
    public enum SpriteType
    {
        floor,
        outerWall
    }
}
