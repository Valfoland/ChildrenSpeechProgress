using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Levels;
using Section1.MissionsDecorator;

namespace Section1.IlluminatorLevels.Level0
{
    public class DataLevelManager : Section1.MissionsDecorator.DataLevelManager
    {
        public DataLevelManager(int countRounds) : base(countRounds)
        {
            dataLevel = new DataHome();
            InstantiateData();
            GetDataFromJson($"JsonDataIlluminatorLevels/JsonDataIlluminatorLevel{idLvl}");
            GetCommonDataFromJson("JsonDataIlluminatorLevels", "JsonCommonDataIllumLevels");
            dataLevel.SpriteDict = LoadSprites(dataLevel.NameDirDict);
            SpriteDict = dataLevel.SpriteDict;
        }
    }
}
