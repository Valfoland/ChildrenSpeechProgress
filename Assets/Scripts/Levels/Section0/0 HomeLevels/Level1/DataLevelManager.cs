using System.Collections.Generic;
using Levels;

namespace Section0.HomeLevels.Level1
{
    public class DataLevel
    {
        public string NameMission = "Home";
        public List<string> NameDirList = new List<string>
        {
            "а",
            "б",
            "в",
            "г",
            "д",
            "к",
            "м",
            "н",
            "п",
            "т",
            "ф"
        };
    }

    public sealed class DataLevelManager : Levels.DataLevelManager
    {
        private DataLevel dataLevel;
        public DataLevelManager()
        {
            dataLevel = new DataLevel();
            InstantiateData(dataLevel.NameDirList, dataLevel.NameMission);
        }
    }
}
