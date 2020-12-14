using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Levels;

namespace Section1.HelpAstronautLevels.Level0
{
    public class DataHome
    {
        public Dictionary<string, List<string>> NameDirDict = new Dictionary<string, List<string>>();
    }

    public class DataLevelManager : Levels.DataLevelManager
    {
        public List<KeyValuePair<string, string>> NameItemsPair = new List<KeyValuePair<string, string>>();
        private List<Dictionary<string, List<string>>> nameItemsList;
        private DataHome dataLevel;
        private int countRounds;

        public DataLevelManager(int countRounds)
        {
            this.countRounds = countRounds;
            dataLevel = new DataHome();
            GetDataFromJson();
            InstantiateData(dataLevel.NameDirDict);
        }

        protected sealed override void InstantiateData(Dictionary<string,List<string>> nameDirDict, string startSentence = "")
        {
            base.InstantiateData(nameDirDict);

        }
        
        private void GetDataFromJson()
        {
            JsonParserGame<Dictionary<string, List<string>>> jsonData = new JsonParserGame<Dictionary<string, List<string>>>();
            var path = $"JsonDataHelpAstronautLevels/JsonDataHelpAstronautLevel{idLvl}";
            nameItemsList = new List<Dictionary<string, List<string>>>
            {
                jsonData.GetData(path, "Objectives"),
                jsonData.GetData(path, "Adjectives"),
                jsonData.GetData(path, "Signs")
            };
            ShuffleItemList();

            foreach (var data in nameItemsList)
            {
                dataLevel.NameDirDict = dataLevel.NameDirDict
                    .Concat(data)
                    .ToDictionary(x => x.Key, x => x.Value);
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
