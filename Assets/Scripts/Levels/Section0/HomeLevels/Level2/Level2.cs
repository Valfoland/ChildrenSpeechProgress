using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section0.HomeLevels.Level2
{
    public class Level2 : MonoBehaviour
    {
        public static System.Action<GameObject, Sprite> onInstanceItem;
        public static System.Action onDestroyItem;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Transform parentItem;
        [SerializeField] private SocketItem[] floorItem;

        private string currentLetter;

        private void Start()
        {
            InitData();
            InstanceItem();
        }

        private void InitData()
        {
            SocketItem.TempContainer = GameObject.FindWithTag("Container").transform as RectTransform;
            ILevelData data = new DataLevel2Manager();
            data.InitData();
        }
        
        private void InstanceItem()
        {
            currentLetter = DataLevelManager.DataNameList.Dequeue();
            DataLevelManager.DataNameList.Enqueue(currentLetter);
            
            foreach (var data in DataLevelManager.DataLevelDict[currentLetter])
            {
                GameObject item = Instantiate(itemPrefab, parentItem);
                onInstanceItem?.Invoke(item, data);
            }
        }

        private void DestroyItem()
        {
            onDestroyItem?.Invoke();
        }

        private void CheckSyllable()
        {
            
        }
    }
}
