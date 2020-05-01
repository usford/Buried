using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFloor : MonoBehaviour
{
    private List<Sprite> sprites;

    public Sprite sprite;

    private void Start() 
    {
        if (sprite == null)
        {   
            sprites = GameObject.Find("Random").GetComponent<RandomGenerate>().floorSprites;
            Sprite randomSprite = sprites[Random.Range(0, sprites.Count)];
            GetComponent<SpriteRenderer>().sprite = randomSprite;
        }
    }
}
