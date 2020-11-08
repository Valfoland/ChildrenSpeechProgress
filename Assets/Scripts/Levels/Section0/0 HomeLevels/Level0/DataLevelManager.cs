using System.Collections.Generic;
using Levels;

namespace Section0.HomeLevels.Level0
{
    public class DataLevel
    {
        public string NameMission = "Home";

        public string StartSentence =
            "Привет! Все карточки перемешались! Помоги мне найти вещи, где содержится звук";
        public List<string> NameDirList = new List<string>
        {
            "б-п",
            "в-ф",
            "г-к",
            "д-т",
            "н-м"
        };
    }
    
    public sealed class DataLevelManager : Levels.DataLevelManager
    {
        private DataLevel dataLevel;
        public DataLevelManager()
        {
            dataLevel = new DataLevel();
            InstantiateData(dataLevel.NameDirList, dataLevel.NameMission, dataLevel.StartSentence);
        }
    }
}

