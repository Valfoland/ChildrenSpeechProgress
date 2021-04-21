using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

namespace Section0.LettersBasketsLevels
{
    public class ItemsGenerator : MonoBehaviour
    {
        [SerializeField] private FallenItem fallenItem;
        [SerializeField] private Transform fallenItemsContainer;
        [SerializeField] private int maxCountGenerateItems;
        [SerializeField] private int delayGenerateItem;
        [SerializeField] private int speedItems;

        public Action<string> onItemTake;
        public Action<string> onItemFall;
        
        private Vector2 canvasSize;
        private List<KeyValuePair<string, Sprite>> itemsLoseDataDict;
        private List<KeyValuePair<string, Sprite>> itemsWinDataDict;
        private string winTypeWord;
        private string loseTypeWord;
        private List<FallenItem> fallenItemsObjectList = new List<FallenItem>();
        
        private bool canMove;
        private string currentTypeWords;
        private int capacityFallenItems;
        private double deltaTime;

        public void InitData()
        {
            canvasSize = GameObject.FindWithTag(DataGame.CANVAS_LEVEL).GetComponent<RectTransform>().sizeDelta;
        }
        
        public void EnableGenerateItems(string currentTypeWords, Dictionary<string, Dictionary<string, Sprite>> itemsDataDict)
        {
            this.currentTypeWords = currentTypeWords;

            SeparateDataWords(itemsDataDict);
            InitUniqueLoseDict();
            
            winTypeWord = currentTypeWords;
            Generate();
            
            canMove = true;
        }

        private void SeparateDataWords(Dictionary<string, Dictionary<string, Sprite>> itemsDataDict)
        {
            foreach (var item in itemsDataDict)
            {
                if (item.Key == currentTypeWords)
                {
                    itemsWinDataDict = item.Value.ToList();
                }
                else
                {
                    itemsLoseDataDict = item.Value.ToList();
                    loseTypeWord = item.Key;
                }
            }
        }

        private void InitUniqueLoseDict()
        {
            foreach (var item in itemsWinDataDict)
            {
                for (int i = 0; i < itemsLoseDataDict.Count; i++)
                {
                    if (item.Key == itemsLoseDataDict[i].Key)
                    {
                        itemsLoseDataDict.RemoveAt(i);
                    }
                }
            }
        }

        public void DisableGenerateItems()
        {
            foreach (var fallenItemObject in fallenItemsObjectList)
            {
                Destroy(fallenItemObject.gameObject);
            }
            
            fallenItemsObjectList.Clear();
            capacityFallenItems = 0;
            canMove = false;
        }
        
        private void Update()
        {
            if(!canMove) return;
            MoveItems();
        }
        
        private void Generate()
        {
            for (int i = 0; i < maxCountGenerateItems; i++)
            {
                var fallenItemObject = Instantiate(fallenItem, fallenItemsContainer);
                SetDataToItem(fallenItemObject);
                fallenItemsObjectList.Add(fallenItemObject);
            }
        }
        
        private void SetDataToItem(FallenItem fallenItemObject)
        { 
            fallenItemObject.CanFall = false;
            var randomKeyId = Random.Range(0, 2);
            var itemsDataDict = randomKeyId == 0 ? itemsWinDataDict : itemsLoseDataDict;
            var randomValueId = Random.Range(0, itemsDataDict.Count);
            
            fallenItemObject.TypeWord = randomKeyId == 0 ? winTypeWord : loseTypeWord;
            fallenItemObject.onTouchBasket = () =>
            {
                onItemTake?.Invoke(fallenItemObject.TypeWord);
                SetDataToItem(fallenItemObject);
            };
            
            if (itemsDataDict[randomValueId].Value == null)
            {
                fallenItemObject.TextWord.text = itemsDataDict[randomValueId].Key;
            }
            else
            {
                fallenItemObject.ImageWord.sprite = itemsDataDict[randomValueId].Value;
            }

            fallenItemObject.ItemRectTransform.anchoredPosition = new Vector2(
                    Random.Range(-canvasSize.x / 2 + 50, canvasSize.x / 2 - 50),
                    canvasSize.y / 2 + 100
                    );
                
        }

        private void MoveItems()
        {
            if (Time.time - deltaTime >= delayGenerateItem)
            {
                deltaTime = Time.time;
                
                for(int i = 0; i < fallenItemsObjectList.Count; i++)
                {
                    if (fallenItemsObjectList[i].CanFall == false)
                    {
                        fallenItemsObjectList[i].CanFall = true;
                        break;
                    }
                }
            }

            for(int i = 0; i < fallenItemsObjectList.Count; i++)
            {
                if(fallenItemsObjectList[i] == null || 
                   fallenItemsObjectList[i].CanFall == false) continue;
                
                fallenItemsObjectList[i].ItemRectTransform.anchoredPosition = Vector2.MoveTowards(
                    fallenItemsObjectList[i].ItemRectTransform.anchoredPosition,
                    new Vector2(fallenItemsObjectList[i].ItemRectTransform.anchoredPosition.x, -canvasSize.y / 2 - 100), 
                    Time.deltaTime * speedItems
                );

                if (fallenItemsObjectList[i].ItemRectTransform.anchoredPosition.y <= -canvasSize.y / 2 - 90)
                {
                    SetDataToItem(fallenItemsObjectList[i]);
                    onItemFall?.Invoke(fallenItemsObjectList[i].TypeWord);
                }
            }
        }
    }
}

