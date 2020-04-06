using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDistributor : MonoBehaviour
{
    private GameObject line;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private GameObject storagePrefab;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform parentLinePrefab;
    [SerializeField] private Transform parentStoragePrefab;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ItemData itemData;

    [Header("Количество корзин и предметов")]
    [SerializeField] private int countTypeItems;
    [SerializeField] private int countItems;

    private int countToResetItems;
    public static System.Action onInitStorage;

    private void Awake()
    {
        GameManager.onInstanceObjects += InstanceObjects;
        DragableObject.onTriggerObjects += CheckCollision;
    }

    private void OnDestroy()
    {
        GameManager.onInstanceObjects -= InstanceObjects;
        DragableObject.onTriggerObjects -= CheckCollision;
    }

    private void CheckCollision()
    {
        bool isCanDrope = false;
        if (DragableObject.DragableObjects[0].gameObject.layer != 8)
        {
            DragableObject.DragableObjects.Insert(0, DragableObject.DragableObjects[1]);
        }
        isCanDrope = DragableObject.DragableObjects[0] + DragableObject.DragableObjects[1];
        if (isCanDrope)
        {
            bool isTrueType = DragableObject.DragableObjects[0] == DragableObject.DragableObjects[1];
            if (isTrueType)
            {
                gameManager.IterScore();
                DragableObject.DragableObjects[1].VisibleCollision(true);
            }
            else
            {
                DragableObject.DragableObjects[1].VisibleCollision(false);
            }
            countToResetItems++;
            DragableObject.DragableObjects[0].VisibleFalse();
        }

        if (countToResetItems >= countItems)
        {
            countToResetItems = 0;
            gameManager.InstanceObjects(false);
        }
    }

    private void ChooseItems()
    {
        string itemName;
        ItemData.ItemNameList.Clear();
        for (int i = 0; i < countTypeItems; i++)
        {
            itemName = itemData.ItemNameQueue.Dequeue();
            itemData.ItemNameQueue.Enqueue(itemName);
            ItemData.ItemNameList.Add(itemName);
        }
    }

    private void InstanceObjects(bool isFirstInstance)
    {
        ChooseItems();
        if(line != null)
        {
            Destroy(line);
        }
        line = Instantiate(linePrefab, parentLinePrefab);
        Transform lineChild;

        lineChild = line.transform.GetChild(1);
        for (int i = 0; i < countItems; i++)
        {
           Instantiate(itemPrefab, lineChild);
        }
        for (int i = 0; i < countTypeItems && isFirstInstance; i++)
        {
            Instantiate(storagePrefab, parentStoragePrefab);
        }
        onInitStorage?.Invoke();
    }
}
