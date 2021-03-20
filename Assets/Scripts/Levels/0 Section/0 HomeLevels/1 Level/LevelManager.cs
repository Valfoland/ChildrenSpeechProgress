using System;
using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;
using Levels;

namespace Section0.HomeLevels.Level1
{
    public class LevelManager : LevelProduct
    {
        public static Action onInit;

        [SerializeField] private ItemLevel itemPrefab;
        [SerializeField] private GameObject pagePrefab;
        [SerializeField] private GameObject fieldPrefab;
        [SerializeField] private Transform parentField;
        [SerializeField] private SocketItem[] floorItem;
        
        private List<ItemLevel> itemLevelList = new List<ItemLevel>();
        
        private GameObject currentField;
        private GameObject currentCarousel;
        
        private string currentLetter;
        private int countNeedSprite;
        private int currentIdPack;
        private int countPut;

        private const int CAPASITY_FIELD = 8;
        private const string TAG_FIELD = "Field";
        private const string TAG_CONTAINER = "Container";

        private DataLevelManager dataLevelManager;
        
        protected override void Start()
        {
            base.Start();
            InitData();
            ReshapeItems();
        }

        private void OnDestroy()
        {
            SocketItem.onPut -= CheckSyllable;
        }

        private void InitData()
        {
            SocketItem.TempContainer = GameObject.FindWithTag(TAG_CONTAINER).transform as RectTransform;
            voiceButton.onClick.AddListener(ClickButtonVoice);
            dataLevelManager = new DataLevelManager();
            SocketItem.onPut += CheckSyllable;
        }

        private void ReshapeItems()
        {
            countNeedSprite = 0;
            
            if (currentIdPack < dataLevelManager.LevelSpriteDict.Count)
            {
                DestroyItems();
                
                currentIdPack++;
                currentLetter = dataLevelManager.LevelKeySpriteList.Dequeue();
                dataLevelManager.LevelKeySpriteList.Enqueue(currentLetter);

                if (dataLevelManager.LevelSpriteDict[currentLetter].Count == 0 )
                {
                    CheckWinLevel();
                }

                ReshapeField();
                ReshapeImages();
                Voice("звук");
                Voice(currentLetter);
                
                if (dataLevelManager.LevelSpriteDict[currentLetter].Count != 0)
                {
                    Invoke("InitCarousel", 0.2f);
                }
            }
            else
            {
                currentIdPack = 0;
                CheckWinLevel();
            }
        }

        private void ReshapeField()
        {
            currentCarousel = Instantiate(fieldPrefab, parentField);

            foreach (Transform child in currentCarousel.transform)
            {
                if (child.CompareTag(TAG_FIELD))
                {
                    currentField = child.gameObject;
                    break;
                }
            }
        }
        
        private void ReshapeImages()
        {
            Transform pageItem = null;
            int i = 0;
                
            foreach (var data in dataLevelManager.LevelSpriteDict[currentLetter])
            {
                if (i % CAPASITY_FIELD == 0)
                {
                    i = 0;
                    pageItem = Instantiate(pagePrefab, currentField.transform).transform;
                }
                i++;
                    
                var item = Instantiate(itemPrefab, pageItem);
                itemLevelList.Add(item);
                item.SetData(data, currentLetter);
            }
        }
        
        private void CheckSyllable(SocketItem item)
        {
            SocketItem tempFloor;
            if (item.gameObject.name.IndexOf(currentLetter, StringComparison.OrdinalIgnoreCase) == 0)
                tempFloor = floorItem[0];
            else if (item.gameObject.name.IndexOf(currentLetter, StringComparison.OrdinalIgnoreCase) + 1 == item.gameObject.name.Length)
                tempFloor = floorItem[2];
            else
                tempFloor = floorItem[1];
            
            if (item.transform.parent == tempFloor.transform)
            {
                countNeedSprite++;

                SetInteractItems(item, true);
                AttemptCounter.SetAttempt(true);

                if (countNeedSprite >= dataLevelManager.LevelSpriteDict[currentLetter].Count)
                {
                    ReshapeItems();
                }
            }
            else if(item.transform.parent != tempFloor.transform )
            {
                SetInteractItems(item, false);
                AttemptCounter.SetAttempt(false);
                item.BackToStartPos();
            }
        }

        private void SetInteractItems(SocketItem item, bool isRightPlace)
        {
            foreach (var i in itemLevelList)
            {
                if (i.gameObject == item.gameObject)
                {
                    i.SetInteractable(isRightPlace);
                }
            }
        }

        private void InitCarousel()
        {
            onInit?.Invoke();
        }

        private void ClickButtonVoice()
        {
            SoundSource.VoiceSound(currentLetter);
        }

        private void DestroyItems()
        {
            Destroy(currentCarousel);
            foreach (var i in itemLevelList)
            {
                Destroy(i);
            }
        }

    }
}
