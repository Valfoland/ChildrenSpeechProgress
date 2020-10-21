using System;
using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;

namespace Section0.HomeLevels
{
    public class HomeLevel2 : LevelProduct
    {
        public static Action<GameObject, Sprite, string> onInstanceItem;
        public static Action onInit;
        public static Action onDestroy;
        public static Action<GameObject, bool> onPutItem;

        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private GameObject pagePrefab;
        [SerializeField] private GameObject fieldPrefab;
        [SerializeField] private Transform parentField;
        [SerializeField] private SocketItem[] floorItem;
        private GameObject currentField;
        private GameObject currentCarousel;
        
        private string currentLetter;
        private int countNeedSprite;
        private int currentIdPack;
        private int countPut;

        private const int CAPASITY_FIELD = 8;
        private const string TAG_FIELD = "Field";
        private const string TAG_CONTAINER = "Container";

        private DataHomeLevel2Manager dataHomeLevel2Manager;
        
        private void Start()
        {
            InitData();
            ReshapeItems();
        }

        private void InitData()
        {
            SocketItem.TempContainer = GameObject.FindWithTag(TAG_CONTAINER).transform as RectTransform;
            dataHomeLevel2Manager = new DataHomeLevel2Manager();

            SocketItem.onPut += CheckSyllable;
        }

        private void OnDestroy()
        {
            SocketItem.onPut -= CheckSyllable;
        }

        private void ReshapeItems()
        {
            countNeedSprite = 0;
            
            if (currentIdPack < dataHomeLevel2Manager.DataLevelDict.Count)
            {
                DestroyItems();
                
                currentIdPack++;
                currentLetter = dataHomeLevel2Manager.DataNameList.Dequeue();
                dataHomeLevel2Manager.DataNameList.Enqueue(currentLetter);

                if (dataHomeLevel2Manager.DataLevelDict[currentLetter].Count == 0 )
                {
                    CheckWinLevel();
                }

                ReshapeField();
                ReshapeImages();
                Voice("звук");
                Voice(currentLetter);
                
                if (dataHomeLevel2Manager.DataLevelDict[currentLetter].Count != 0)
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
                
            foreach (var data in dataHomeLevel2Manager.DataLevelDict[currentLetter])
            {
                if (i % CAPASITY_FIELD == 0)
                {
                    i = 0;
                    pageItem = Instantiate(pagePrefab, currentField.transform).transform;
                }
                i++;
                    
                GameObject item = Instantiate(itemPrefab, pageItem);
                onInstanceItem?.Invoke(item, data, currentLetter);
            }
        }
        
        private void InitCarousel()
        {
            onInit?.Invoke();
        }

        private void Voice(string word)
        {
            SoundSource.VoiceSound(word);
        }

        private void DestroyItems()
        {
            Destroy(currentCarousel);
            onDestroy?.Invoke();
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
                onPutItem?.Invoke(item.gameObject, true);
                AttemptCounter.SetAttempt(true);

                if (countNeedSprite >= dataHomeLevel2Manager.DataLevelDict[currentLetter].Count)
                {
                    ReshapeItems();
                }
            }
            else if(item.transform.parent != tempFloor.transform )
            {
                onPutItem?.Invoke(item.gameObject, false);
                AttemptCounter.SetAttempt(false);
                item.BackToStartPos();
            }
        }
    }
}
