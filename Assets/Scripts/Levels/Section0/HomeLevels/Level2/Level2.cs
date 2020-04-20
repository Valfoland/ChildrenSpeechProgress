using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section0.HomeLevels.Level2
{
    public class Level2 : LevelManager
    {
        public static Action<GameObject, Sprite> onInstanceItem;
        public static Action onInit;
        public static Action onDestroy;
        public static Action<GameObject> onPutItem;

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
        
        private const int CAPASITY_FIELD = 8;
        private const string TAG_FIELD = "Field";
        private const string TAG_CONTAINER = "Container";

        private void Start()
        {
            InitData();
            ReshapeItems();
        }

        private void InitData()
        {
            SocketItem.TempContainer = GameObject.FindWithTag(TAG_CONTAINER).transform as RectTransform;
            ILevelData data = new DataLevel2Manager();
            data.InitData();

            SocketItem.onPut += CheckSyllable;
        }

        private void OnDestroy()
        {
            SocketItem.onPut -= CheckSyllable;
        }

        private void ReshapeItems()
        {
            countNeedSprite = 0;
            
            if (currentIdPack < DataLevelManager.DataLevelDict.Count)
            {
                DestroyItems();
                Voice();
                
                currentIdPack++;
                currentLetter = DataLevelManager.DataNameList.Dequeue();
                DataLevelManager.DataNameList.Enqueue(currentLetter);

                ReshapeField();
                ReshapeImages();
                
                if (DataLevelManager.DataLevelDict[currentLetter].Count != 0)
                {
                    Invoke("InitCarousel", 0.2f);
                }
            }
            else
            {
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
                
            foreach (var data in DataLevelManager.DataLevelDict[currentLetter])
            {
                if (i % CAPASITY_FIELD == 0)
                {
                    i = 0;
                    pageItem = Instantiate(pagePrefab, currentField.transform).transform;
                }
                i++;
                    
                GameObject item = Instantiate(itemPrefab, pageItem);
                onInstanceItem?.Invoke(item, data);
            }
        }
        
        private void InitCarousel()
        {
            onInit?.Invoke();
        }

        private void Voice()
        {
            onVoice?.Invoke(""); //TODO считывание с data level manager
        }

        private void DestroyItems()
        {
            Destroy(currentCarousel);
            onDestroy?.Invoke();
        }

        private void CheckSyllable(SocketItem item)
        {
            SocketItem tempFloor;
            if (item.gameObject.name.IndexOf(currentLetter, StringComparison.Ordinal) == 0)
                tempFloor = floorItem[0];
            else if (item.gameObject.name.IndexOf(currentLetter, StringComparison.Ordinal) + 1 == item.gameObject.name.Length)
                tempFloor = floorItem[2];
            else
                tempFloor = floorItem[1];
            
            if (item.transform.parent == tempFloor.transform)
            {
                countNeedSprite++;
                onPutItem?.Invoke(item.gameObject);
                AttemptCounter.SetAttempt(true);

                if (countNeedSprite >= DataLevelManager.DataLevelDict[currentLetter].Count)
                {
                    ReshapeItems();
                }
                
            }
            else
            {
                AttemptCounter.SetAttempt(false);
                item.BackToStartPos();
            }
        }

        private void CheckWinLevel()
        {
            currentIdPack = 0;
            onEndLevel?.Invoke(AttemptCounter.IsLevelPass());
        }
    }
}
