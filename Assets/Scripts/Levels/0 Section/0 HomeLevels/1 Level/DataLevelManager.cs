using System.Collections.Generic;
using Levels;

namespace Section0.HomeLevels.Level1
{
    public class DataLevel
    {
        public string NameMission = "Home";
        public Dictionary<string, List<string>> NameDirDict = new Dictionary<string, List<string>>();
    }

    public sealed class DataLevelManager : Levels.DataLevelManager
    {
        private DataLevel dataLevel;
        public DataLevelManager()
        {
            dataLevel = new DataLevel();
            GetDataFromJson();
            InstantiateData(dataLevel.NameDirDict, dataLevel.NameMission);
        }

        private void GetDataFromJson()
        {
            JsonParserGame<Dictionary<string, List<string>>> jsonData = new JsonParserGame<Dictionary<string, List<string>>>();
            var dataText = jsonData.GetData("JsonDataHomeLevels","JsonDataHomeLevel1");
            dataLevel.NameDirDict = dataText;
        }
    }
}
