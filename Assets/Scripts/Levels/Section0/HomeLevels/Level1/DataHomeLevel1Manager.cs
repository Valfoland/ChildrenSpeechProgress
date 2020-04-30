
using System.Collections.Generic;

namespace Section0.HomeLevels
{
    public class DataHomeLevel1
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
    
    public class DataHomeLevel1Manager : DataLevelManager, ILevelData
    {
        private DataHomeLevel1 dataHomeLevel1;
        public void InitData()
        {
            dataHomeLevel1 = new DataHomeLevel1();
            InstanceData(dataHomeLevel1.NameDirList, dataHomeLevel1.NameMission, dataHomeLevel1.StartSentence);
        }
    }
}

