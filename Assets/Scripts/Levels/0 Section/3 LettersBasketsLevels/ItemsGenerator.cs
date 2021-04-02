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

        public Action<string> onItemTake;
        public Action<string> onItemFall;
        
        private Vector2 canvasSize;
        private List<KeyValuePair<string, Sprite>> itemsDataList;
        private List<FallenItem> fallenItemsObjectlist = new List<FallenItem>();
        private bool canGenerate;
        private string currentTypeWords;

        public void InitData()
        {
            canvasSize = GameObject.FindWithTag(DataGame.CANVAS_LEVELS).GetComponent<RectTransform>().sizeDelta;
        }
        
        public void EnableGenerateItems(string currentTypeWords, Dictionary<string, Sprite> itemsDataDict)
        {
            Generate();
            this.currentTypeWords = currentTypeWords;
            itemsDataList = itemsDataDict.ToList();
            canGenerate = true;
        }

        public void DisableGenerateItems()
        {
            foreach (var fallenItemObject in fallenItemsObjectlist)
            {
                Destroy(fallenItemObject.gameObject);
            }
            fallenItemsObjectlist.Clear();
            canGenerate = false;
        }
        
        private void Update()
        {
            if(!canGenerate) return;
            MoveItems();
        }
        
        private void Generate()
        {
            fallenItemsObjectlist.Clear();
            
            for (int i = 0; i < maxCountGenerateItems; i++)
            {
                var fallenItemObject = Instantiate(fallenItem, fallenItemsContainer);
                SetDataToItem(fallenItemObject);
                fallenItemsObjectlist.Add(fallenItemObject);
            }
        }
        
        private void SetDataToItem(FallenItem fallenItemObject)
        {
            fallenItemObject.TypeWord = currentTypeWords;
            fallenItemObject.onTouchBasket = () =>
            {
                onItemTake?.Invoke(fallenItemObject.TypeWord);
                SetDataToItem(fallenItemObject);
            };

            var randomId = Random.Range(0, itemsDataList.Count);
            if (itemsDataList[randomId].Value == null)
            {
                fallenItemObject.TextWord.text = itemsDataList[randomId].Key;
            }
            else
            {
                fallenItemObject.ImageWord.sprite = itemsDataList[randomId].Value;
            }

            fallenItemObject.ItemRectTransform.anchoredPosition = new Vector2(
                    Random.Range(-canvasSize.x / 2 + 50, canvasSize.x / 2 - 50),
                    canvasSize.y / 2 + 100
                    );
                
        }

        private void MoveItems()
        {
            foreach (var fallenItemObject in fallenItemsObjectlist)
            {
                fallenItemObject.ItemRectTransform.anchoredPosition = Vector2.MoveTowards(
                    fallenItemObject.ItemRectTransform.anchoredPosition,
                    new Vector2(fallenItemObject.ItemRectTransform.anchoredPosition.x, -canvasSize.y / 2 - 100), 
                    Time.deltaTime
                );

                if (fallenItemObject.ItemRectTransform.anchoredPosition.y <= -canvasSize.y / 2 - 100)
                {
                    onItemFall?.Invoke(fallenItemObject.TypeWord);
                    SetDataToItem(fallenItemObject);
                }
            }
        }
    }
}

