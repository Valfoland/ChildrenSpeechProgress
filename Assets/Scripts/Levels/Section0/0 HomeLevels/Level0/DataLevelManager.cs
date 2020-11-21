using System.Collections.Generic;
using Levels;
using UnityEngine;

namespace Section0.HomeLevels.Level0
{
    public class DataLevel
    {
        public string NameMission = "Home";
        public string StartSentence;
        public List<string> NameDirList = new List<string>();
    }
    
    public sealed class DataLevelManager : Levels.DataLevelManager
    {
        private DataLevel dataLevel;
        public DataLevelManager()
        {
            LoadData();
            InstantiateData(dataLevel.NameDirList, dataLevel.NameMission, dataLevel.StartSentence);
        }

        private void LoadData()
        {
            dataLevel = new DataLevel();
            JsonParserGame<Dictionary<string, List<string>>> jsonData = new JsonParserGame<Dictionary<string, List<string>>>();
            var dataText = jsonData.GetData("JsonDataHomeLevel0");
            
            dataLevel.StartSentence = dataText["StartSentence"][0];
            dataLevel.NameDirList.Clear();
            foreach (var data in dataText.Keys)
            {
                if (!data.StartsWith("StartSentence"))
                {
                    dataLevel.NameDirList.Add(data);
                }
            }
        }
    }
}

