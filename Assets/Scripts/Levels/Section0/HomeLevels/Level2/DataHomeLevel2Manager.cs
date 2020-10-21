
using System.Collections.Generic;

namespace Section0.HomeLevels
{
    public class DataHomeLevel2
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

    public sealed class DataHomeLevel2Manager : DataLevelManager
    {
        private DataHomeLevel2 dataHomeLevel2;
        public DataHomeLevel2Manager()
        {
            dataHomeLevel2 = new DataHomeLevel2();
            InstantiateData(dataHomeLevel2.NameDirList, dataHomeLevel2.NameMission);
        }
    }
}
