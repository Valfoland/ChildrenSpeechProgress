using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Levels;

namespace Section1.MissionsDecorator
{
    public class DataHome
    {
        public Dictionary<string, List<string>> NameDirDict = new Dictionary<string, List<string>>();
        public Dictionary<string, Dictionary<string, Sprite>> SpriteDict = new Dictionary<string, Dictionary<string, Sprite>>(); 
    }

    public class DataLevelManager : Levels.DataLevelManager
    {
        private int countRounds;
        public string StartSentence;
        protected DataHome dataLevel;
        private List<Dictionary<string, List<string>>> nameItemsList;
        public List<KeyValuePair<string, string>> NameItemsPair = new List<KeyValuePair<string, string>>();
        public Dictionary<string, Dictionary<string, Sprite>> SpriteDict;

        public DataLevelManager(int countRounds)
        {
            this.countRounds = countRounds;
        }

        protected virtual void GetDataFromJson(string path)
        {
            JsonParserGame<Dictionary<string, List<string>>> jsonData = new JsonParserGame<Dictionary<string, List<string>>>();
            nameItemsList = new List<Dictionary<string, List<string>>>
            {
                jsonData.GetData(path, "Objectives"),
                jsonData.GetData(path, "Adjectives"),
                jsonData.GetData(path, "Signs")
            };
            ShuffleItemList();

            foreach (var data in nameItemsList)
            {
                try
                {
                    dataLevel.NameDirDict = dataLevel.NameDirDict
                        .Concat(data)
                        .ToDictionary(x => x.Key, x => x.Value);
                }
                catch (ArgumentException)
                {
                    
                }
            }
            
            foreach (var data in dataLevel.NameDirDict)
            {
                for (int i = 0; i < data.Value.Count; i++)
                {
                    if (data.Value[i] == "")
                    {
                        data.Value[i] = data.Key;
                    }
                }
            }
        }

        private void ShuffleItemList()
        {
            var tempPair = new List<KeyValuePair<string, string>>();

            foreach (var item in nameItemsList)
            {
                foreach (var item1 in item)
                {
                    foreach (var item2 in item1.Value)
                    {
                        var tempItem = item2 == "" ? item1.Key : item2;
                        tempPair.Add(new KeyValuePair<string, string>(item1.Key, tempItem));
                    }
                }
            }

            var shuffleArray = tempPair.Count.ShuffleNumbers();

            countRounds = countRounds > tempPair.Count ? tempPair.Count : countRounds;
            
            for(int i = 0; i < countRounds; i++)
            {
                NameItemsPair.Add(tempPair[shuffleArray[i]]);
            }
        }
    }
}
