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
        public List<Dictionary<string, List<string>>> NameItemsList = new List<Dictionary<string, List<string>>>();
        private DataHome dataLevel;

        public DataLevelManager()
        {
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
            NameItemsList[0] = jsonData.GetData($"JsonDataHelpAstronautLevels/JsonDataHelpAstronautLevel{idLvl}", "Objectives");
            NameItemsList[1] = jsonData.GetData($"JsonDataHelpAstronautLevels/JsonDataHelpAstronautLevel{idLvl}", "Adjectives");
            NameItemsList[2] = jsonData.GetData($"JsonDataHelpAstronautLevels/JsonDataHelpAstronautLevel{idLvl}", "Signs");

            foreach (var data in NameItemsList)
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
    }
}
