using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    [SerializeField] private Text textStorage;
    private static List<string> itemList= new List<string>();
    private static int numberOfItem;

    private void Start()
    {
        InitStorage();
        ItemDistributor.onInitStorage += InitStorage;
    }

    private void OnDestroy()
    {
        ItemDistributor.onInitStorage -= InitStorage;
    }

    private void InitStorage()
    {
        itemList = ItemData.ItemNameList;
        gameObject.name = itemList[numberOfItem++];
        textStorage.text = Translator.TransTextDict[gameObject.name];

        if (numberOfItem >= ItemData.ItemNameList.Count)
        {
            numberOfItem = 0;
        }
    }
}
