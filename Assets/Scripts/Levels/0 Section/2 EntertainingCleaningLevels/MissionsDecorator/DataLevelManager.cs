using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Levels;

namespace Section0.EntertainingCleaningLevels.MissionsDecorator
{
    public class DataLevel
    {
        public Dictionary<string, List<string>> NameDirDict = new Dictionary<string, List<string>>();
        public Dictionary<string, Dictionary<string, Sprite>> SpriteDict = new Dictionary<string, Dictionary<string, Sprite>>(); 
    }

    public class DataLevelManager : Levels.DataLevelManager
    {
        protected int countRounds;
        protected DataLevel dataLevel;
        
        public Dictionary<int, List<DialogueData>> DialogueDict = new Dictionary<int, List<DialogueData>>();
        public List<KeyValuePair<string, string>> NameItemsPair = new List<KeyValuePair<string, string>>();
        public Dictionary<string, Dictionary<string, Sprite>> SpriteDict;
        protected Dictionary<string, List<string>> nameItemsList;

        public DataLevelManager(int countRounds)
        {
            this.countRounds = countRounds;
        }

        protected virtual void GetDataFromJson(string path, string fileName)
        {
            JsonParserGame<Dictionary<string, List<string>>> jsonData = new JsonParserGame<Dictionary<string, List<string>>>();
            nameItemsList = jsonData.GetData(path, fileName);
            nameItemsList.Remove("LevelSentences");
            dataLevel.NameDirDict = nameItemsList;
            DialogueDict = LoadDialogueData(nameItemsList["LevelSentences"]);
            ShuffleItemList();

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

        protected virtual void ShuffleItemList()
        {
            var tempPair = new List<KeyValuePair<string, string>>();

            foreach (var item in nameItemsList)
            {
                foreach (var item1 in item.Value)
                {
                    var tempItem = item1 == "" ? item.Key : item1;
                    tempPair.Add(new KeyValuePair<string, string>(item.Key, tempItem));
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
