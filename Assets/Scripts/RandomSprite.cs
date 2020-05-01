using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    private List<Sprite> sprites;
    public Sprite sprite;
    public SpriteType spriteType;

    private void Start() 
    {
        if (sprite == null)
        {   
            switch(spriteType)
            {
                case SpriteType.floor:
                {
                    sprites = GameObject.Find("Random").GetComponent<RandomGenerate>().floorSprites;
                    break;
                }

                case SpriteType.outerWall:
                {
                    sprites = GameObject.Find("Random").GetComponent<RandomGenerate>().outerWallSprites;
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
