using System.Collections.Generic;
using Levels;
using UnityEngine;

namespace Section0.HomeLevels.Level0
{
    public class DataLevel
    {
        public string StartSentence;
        public Dictionary<string, List<string>> NameDirDict = new Dictionary<string, List<string>>();
    }
    
    public sealed class DataLevelManager : Levels.DataLevelManager
    {
        private DataLevel dataLevel;
        public DataLevelManager()
        {
            dataLevel = new DataLevel();
            GetDataFromJson();
            InstantiateData(dataLevel.NameDirDict, dataLevel.StartSentence);
        }

        private void GetDataFromJson()
        {
            JsonParserGame<Dictionary<string, List<string>>> jsonData = new JsonParserGame<Dictionary<string, List<string>>>();
            var dataText = jsonData.GetData("JsonDataHomeLevels","JsonDataHomeLevel0");
            dataLevel.StartSentence = dataText["StartSentence"][0];
            dataText.Remove("StartSentence");
            dataLevel.NameDirDict = dataText;
        }
    }
}

