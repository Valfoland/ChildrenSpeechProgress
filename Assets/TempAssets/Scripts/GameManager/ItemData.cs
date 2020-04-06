using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemData : MonoBehaviour
{
    public static List<string> ItemNameList = new List<string>();
    public static Dictionary<string, List<Sprite>> ItemSpriteDict = new Dictionary<string, List<Sprite>>();
    public Queue<string> ItemNameQueue = new Queue<string>();

    private void Awake()
    {
        ItemNameList.Clear();
        ItemSpriteDict.Clear();
        InitItemQueue();
        InitItemSprite();
    }

    private void InitItemQueue()
    {
        ItemNameQueue.Enqueue("vegetable");
        ItemNameQueue.Enqueue("fruit");
        ItemNameQueue.Enqueue("transport");
        ItemNameQueue.Enqueue("tool");
        ItemNameQueue.Enqueue("furniture");
        ItemNameQueue.Enqueue("plant");
        ItemNameQueue.Enqueue("animal");
        ItemNameQueue.Enqueue("partBody");
    }

    public void InitItemSprite()
    {
        Queue<string> itemNameQueue = new Queue<string>();
        string itemName;

        ItemNameQueue.ToList().ForEach(x => itemNameQueue.Enqueue(x));

        for (int i = 0; i < ItemNameQueue.Count; i++)
        {
            itemName = itemNameQueue.Dequeue();
            ItemSpriteDict.Add(itemName, Resources.LoadAll<Sprite>(itemName).ToList());
        }
    }
}
