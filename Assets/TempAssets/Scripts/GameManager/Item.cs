using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private Image itemSprite;

    private void Start()
    {
        gameObject.name = ItemData.ItemNameList[Random.Range(0, ItemData.ItemNameList.Count)];
        itemSprite = gameObject.GetComponent<Image>();
        itemSprite.sprite = ItemData.ItemSpriteDict[gameObject.name][Random.Range(0,ItemData.ItemSpriteDict[gameObject.name].Count)];
    }
}
