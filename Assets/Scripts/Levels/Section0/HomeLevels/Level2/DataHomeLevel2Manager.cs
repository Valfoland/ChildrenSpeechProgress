
using System.Collections.Generic;

namespace Section0.HomeLevels
{
    public class DataHomeLevel2
    {
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

    public class DataHomeLevel2Manager : DataLevelManager, ILevelData
    {
        private DataHomeLevel2 dataHomeLevel2;
        public void InitData()
        {
            dataHomeLevel2 = new DataHomeLevel2();
            InstanceData(dataHomeLevel2.NameDirList);
        }
    }
}
