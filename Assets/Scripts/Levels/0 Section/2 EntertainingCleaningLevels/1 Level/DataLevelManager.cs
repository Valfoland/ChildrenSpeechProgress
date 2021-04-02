using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Section0.EntertainingCleaningLevels.MissionsDecorator;

namespace Section0.EntertainingCleaningLevels.Level1
{
    public sealed class DataLevelManager : MissionsDecorator.DataLevelManager
    {
        public Queue<KeyValuePair<string, string>> NameItemsPair = new Queue<KeyValuePair<string, string>>();

        public DataLevelManager(int countRounds) : base(countRounds)
        {
            dataLevel = new MissionsDecorator.DataLevel();
            InstantiateData();
            GetDataFromJson("JsonDataEntertainingCleaningLevels", $"JsonDataEntertainingCleaningLevel{idLvl}");
            dataLevel.SpriteDict = LoadSprites(dataLevel.NameDirDict);
            SpriteDict = dataLevel.SpriteDict;
        }

        protected override void ShuffleItemList()
        {
            var tempPair = new List<Queue<KeyValuePair<string, string>>>();
            var tempQueue = new Queue<KeyValuePair<string, string>>();

            var count = 0;
            
            foreach (var item in nameItemsList)
            {
                foreach (var item1 in item.Value)
                {
                    var tempItem = item1 == "" ? item.Key : item1;

                    if (count < 2)
                    {
                        tempQueue.Enqueue(new KeyValuePair<string, string>(item.Key, tempItem));
                    }
                }

                count++;
                if (count >= 2)
                {
                    tempPair.Add(tempQueue);
                    tempQueue = new Queue<KeyValuePair<string, string>>();
                    count = 0;
                }
            }

            var shuffleArray = tempPair.Count.ShuffleNumbers();

            for (int i = 0; i < tempPair.Count; i++)
            {
                if (tempPair[shuffleArray[i]].Count == 2)
                {
                    NameItemsPair.Enqueue(tempPair[shuffleArray[i]].Dequeue());
                    NameItemsPair.Enqueue(tempPair[shuffleArray[i]].Dequeue());
                }
            }
            
            
        }
    }
}
        
